using BankingWallet.Application.Wallet.Interfaces;
using BankingWallet.Domain.Wallet.Entities;
using BankingWallet.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using WalletObj = BankingWallet.Domain.Wallet.Entities.Wallet;


namespace BankingWallet.Infrastructure.Wallet.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly BankingWalletDbContext _dbContext;

    public WalletRepository(BankingWalletDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public WalletObj? GetById(Guid id)
    {
        // Assuming Wallets are stored in a DbSet<Wallet> or its derived classes
        return _dbContext.Wallets
                         .Include(w => w.Transactions) // optional: eager load transactions
                         .FirstOrDefault(w => w.Id == id);
    }

    public async Task UpdateAsync(WalletObj wallet)
    {
        _dbContext.Wallets.Update(wallet);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddTransactionAsync(Transaction transaction)
    {
        _dbContext.Transactions.Add(transaction);
        await _dbContext.SaveChangesAsync();
    }

    public IEnumerable<WalletObj> GetAll()
    {
        return _dbContext.Wallets.Include(w => w.Transactions).ToList();
    }
}