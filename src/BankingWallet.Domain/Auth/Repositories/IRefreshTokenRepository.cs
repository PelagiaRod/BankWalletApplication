using BankingWallet.Domain.Auth.Entities;

namespace BankingWallet.Domain.Auth.Repositories;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken refreshToken);
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task<IEnumerable<RefreshToken>> GetActiveByUserIdAsync(Guid userId);
    Task UpdateAsync(RefreshToken refreshToken);
}