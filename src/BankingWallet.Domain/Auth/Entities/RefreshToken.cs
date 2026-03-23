using BankingWallet.Domain.Common;

namespace BankingWallet.Domain.Auth.Entities;

public class RefreshToken : Entity
{
    public string Token { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public bool IsRevoked { get; private set; }

    public RefreshToken(string token, Guid userId, DateTime expiresAt)
        : base(Guid.NewGuid())
    {
        Token = token;
        UserId = userId;
        ExpiresAt = expiresAt;
        IsRevoked = false;
    }

    public void Revoke() => IsRevoked = true;
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public bool IsActive => !IsRevoked && !IsExpired;
}