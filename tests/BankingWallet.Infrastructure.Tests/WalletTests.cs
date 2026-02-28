using BankingWallet.Domain.Entities;
using BankingWallet.Domain.ValueObjects;
using BankingWallet.Infrastructure.Persistence.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Xunit;


namespace BankingWallet.Infrastructure.Tests;

public class WalletTests
{
    [Fact]
    public void CanAddWallet()
    {
        // 1️⃣ Create and open SQLite in-memory connection
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        // 2️⃣ Configure DbContext
        var options = new DbContextOptionsBuilder<BankingWalletDbContext>()
            .UseSqlite(connection)
            .Options;

        // 3️⃣ Create schema
        using (var context = new BankingWalletDbContext(options))
        {
            context.Database.EnsureCreated();
        }

        // 4️⃣ Act
        using (var context = new BankingWalletDbContext(options))
        {
            var wallet = new FiatWallet(
                Guid.NewGuid(),
                new Money(100, Currency.USD));

            context.Wallets.Add(wallet);
            context.SaveChanges();
        }

        // 5️⃣ Assert
        using (var context = new BankingWalletDbContext(options))
        {
            var count = context.Wallets.Count();
            Assert.Equal(1, count);
        }
    }
}
