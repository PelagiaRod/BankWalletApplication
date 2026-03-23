using BankingWallet.Domain.Common;

namespace BankingWallet.Domain.Auth.Entities;

public sealed class UnauthorizedException : DomainException
{
    public UnauthorizedException()
        : base("User is unauthorised.")
    {
    }
}