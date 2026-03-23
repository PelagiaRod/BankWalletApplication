using BankingWallet.Domain.Auth.Entities;
using BankingWallet.Domain.Auth.Repositories;
using BankingWallet.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BankingWallet.Infrastructure.Auth.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BankingWalletDbContext _context;

    public UserRepository(BankingWalletDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}