using BankingWallet.Application.Services;
using BankingWallet.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BankingWallet.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register your application services here
            services.AddScoped<WalletAppService>();
            services.AddScoped<WalletTransferService>();

            return services;
        }
    }
}
