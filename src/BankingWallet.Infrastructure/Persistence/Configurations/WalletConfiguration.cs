using BankingWallet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("Wallets");

        builder.HasKey(w => w.Id);

        // Value Object: Money
        builder.OwnsOne(w => w.Balance, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("BalanceAmount")
                .HasPrecision(18, 2)
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("BalanceCurrency")
                .IsRequired();
        });

        // Backing field for transactions
        // builder
        //     .HasMany<Transaction>("_transactions")
        //     .WithOne()
        //     .HasForeignKey(t => t.WalletId)
        //     .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(w => w.Transactions)
       .WithOne() // or .WithOne(t => t.Wallet) if Transaction has a Wallet navigation
       .HasForeignKey(t => t.WalletId);

        // builder.Navigation("_transactions")
        //     .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
