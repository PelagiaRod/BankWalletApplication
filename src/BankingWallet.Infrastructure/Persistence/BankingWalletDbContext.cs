using Microsoft.EntityFrameworkCore;
using BankingWallet.Domain.Entities;

namespace BankingWallet.Infrastructure.Persistence.Migrations;

public class BankingWalletDbContext : DbContext
{
    public DbSet<Wallet> Wallets => Set<Wallet>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

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
