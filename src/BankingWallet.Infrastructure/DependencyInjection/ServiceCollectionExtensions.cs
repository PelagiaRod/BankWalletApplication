using BankingWallet.Application.Auth.Interfaces;
using BankingWallet.Application.Auth.Services;
using BankingWallet.Application.Wallet.Interfaces;
using BankingWallet.Domain.Auth.Repositories;
using BankingWallet.Infrastructure.Auth.Repositories;
using BankingWallet.Infrastructure.Auth.Services;
using BankingWallet.Infrastructure.Persistence;
using BankingWallet.Infrastructure.Wallet.Repositories;
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
        services.AddScoped<IUserRepository, UserRepository>();

        // 3️⃣ Register services
        services.AddScoped<IAuthServices, AuthService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        return services;
    }
}