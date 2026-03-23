using Microsoft.EntityFrameworkCore;
using BankingWallet.Domain.Wallet.Entities;
using BankingWallet.Domain.Auth.Entities;
using WalletObj = BankingWallet.Domain.Wallet.Entities.Wallet;

namespace BankingWallet.Infrastructure.Persistence;

public class BankingWalletDbContext : DbContext
{
    public DbSet<WalletObj> Wallets => Set<WalletObj>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<User> Users => Set<User>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public BankingWalletDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankingWalletDbContext).Assembly);

        modelBuilder.Entity<FiatWallet>();
        modelBuilder.Entity<CryptoWallet>();

    }
}
