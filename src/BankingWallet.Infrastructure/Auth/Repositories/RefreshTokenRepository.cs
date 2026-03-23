using BankingWallet.Domain.Auth.Entities;
using BankingWallet.Domain.Auth.Repositories;
using BankingWallet.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BankingWallet.Infrastructure.Auth.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly BankingWalletDbContext _context;

    public RefreshTokenRepository(BankingWalletDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _context.RefreshTokens
            .FirstOrDefaultAsync(t => t.Token == token);
    }

    public async Task<IEnumerable<RefreshToken>> GetActiveByUserIdAsync(Guid userId)
    {
        return await _context.RefreshTokens
            .Where(t => t.UserId == userId && !t.IsRevoked)
            .ToListAsync();
    }

    public async Task UpdateAsync(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Update(refreshToken);
        await _context.SaveChangesAsync();
    }
}