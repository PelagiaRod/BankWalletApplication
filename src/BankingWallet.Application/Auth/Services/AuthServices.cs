using BankingWallet.Application.Auth.DTOs;
using BankingWallet.Application.Auth.Interfaces;
using BankingWallet.Domain.Auth.Entities;
using BankingWallet.Domain.Auth.Repositories;

namespace BankingWallet.Application.Auth.Services;

public class AuthService : IAuthServices
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public AuthService(
        IUserRepository userRepository,
        IJwtTokenService jwtTokenService,
        IPasswordHasher passwordHasher,
        IRefreshTokenRepository refreshTokenRepository)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
        _passwordHasher = passwordHasher;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<AuthResponse> Register(RegisterRequest request)
    {
        var hashedPassword = _passwordHasher.Hash(request.Password);
        var user = new User(request.Email, hashedPassword, request.FullName);
        await _userRepository.AddAsync(user);
        var token = _jwtTokenService.GenerateToken(user);

        var refreshToken = new RefreshToken(token.RefreshToken, user.Id, DateTime.UtcNow.AddDays(7));
        await _refreshTokenRepository.AddAsync(refreshToken);

        return new AuthResponse(token.AccessToken, token.RefreshToken, token.ExpiresAt);
    }

    public async Task<AuthResponse> Login(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedException();

        var token = _jwtTokenService.GenerateToken(user);

        var refreshToken = new RefreshToken(token.RefreshToken, user.Id, DateTime.UtcNow.AddDays(7));
        await _refreshTokenRepository.AddAsync(refreshToken);

        return new AuthResponse(token.AccessToken, token.RefreshToken, token.ExpiresAt);
    }

    public async Task<AuthResponse> RefreshToken(string refreshToken)
    {
        var storedToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);

        if (storedToken is null || !storedToken.IsActive)
            throw new UnauthorizedException();

        // Revoke old token
        storedToken.Revoke();
        await _refreshTokenRepository.UpdateAsync(storedToken);

        // Get user and generate new tokens
        var user = await _userRepository.GetByIdAsync(storedToken.UserId);
        if (user is null)
            throw new UnauthorizedException();

        var tokens = _jwtTokenService.GenerateToken(user);

        // Save new refresh token
        var newRefreshToken = new RefreshToken(
            tokens.RefreshToken,
            user.Id,
            DateTime.UtcNow.AddDays(7));

        await _refreshTokenRepository.AddAsync(newRefreshToken);

        return new AuthResponse(tokens.AccessToken, tokens.RefreshToken, tokens.ExpiresAt);
    }

    public async Task Logout(string userId)
    {
        var tokens = await _refreshTokenRepository.GetActiveByUserIdAsync(Guid.Parse(userId));
        foreach (var token in tokens)
        {
            token.Revoke();
            await _refreshTokenRepository.UpdateAsync(token);
        }
    }
}