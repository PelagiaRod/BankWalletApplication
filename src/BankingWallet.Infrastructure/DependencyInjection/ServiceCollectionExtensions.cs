using BankingWallet.Application.Interfaces;
using BankingWallet.Infrastructure.Persistence.Migrations;
using BankingWallet.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankingWallet.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
    {
        // 1️⃣ Register DbContext
        services.AddDbContext<BankingWalletDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("BankingWalletDb")));

        // 2️⃣ Register repositories
        services.AddScoped<IWalletRepository, WalletRepository>();

        // 3️⃣ Any other infrastructure services can be registered here
        // e.g., services.AddScoped<IEmailSender, EmailSender>();

        return services;
    }
}