using BankingWallet.Domain.Common;

namespace BankingWallet.Domain.Auth.Entities;

public class User : Entity
{
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string FullName { get; private set; }

    public User(string email, string passwordHash, string fullName)
        : base(Guid.NewGuid())
    {
        Email = email;
        PasswordHash = passwordHash;
        FullName = fullName;
    }
}