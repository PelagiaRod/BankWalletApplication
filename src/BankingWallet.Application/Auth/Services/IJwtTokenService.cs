using BankingWallet.Domain.Auth.Entities;

namespace BankingWallet.Application.Auth.Services;

public interface IJwtTokenService
{
    TokenResult GenerateToken(User user);
}

public record TokenResult(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt
);