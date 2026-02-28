using BankingWallet.Application.Interfaces;
using BankingWallet.Domain.Entities;
using BankingWallet.Infrastructure.Persistence.Migrations;
using Microsoft.EntityFrameworkCore;

namespace BankingWallet.Infrastructure.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly BankingWalletDbContext _dbContext;

    public WalletRepository(BankingWalletDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Wallet? GetById(Guid id)
    {
        // Assuming Wallets are stored in a DbSet<Wallet> or its derived classes
        return _dbContext.Wallets
                         .Include(w => w.Transactions) // optional: eager load transactions
                         .FirstOrDefault(w => w.Id == id);
    }

    public async Task UpdateAsync(Wallet wallet)
    {
        _dbContext.Wallets.Update(wallet);
        await _dbContext.SaveChangesAsync();
    }

    public void AddTransaction(Transaction transaction)
    {
        _dbContext.Transactions.Add(transaction);
        _dbContext.SaveChanges();
    }

    public IEnumerable<Wallet> GetAll()
    {
        return _dbContext.Wallets.Include(w => w.Transactions).ToList();
    }
}