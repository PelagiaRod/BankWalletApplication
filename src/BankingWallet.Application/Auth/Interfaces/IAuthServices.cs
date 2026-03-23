using BankingWallet.Application.Auth.DTOs;

namespace BankingWallet.Application.Auth.Interfaces;

public interface IAuthServices
{
    Task<AuthResponse> Register(RegisterRequest request);
    Task<AuthResponse> Login(LoginRequest request);
    Task<AuthResponse> RefreshToken(string refreshToken);
    Task Logout(string userId);
}