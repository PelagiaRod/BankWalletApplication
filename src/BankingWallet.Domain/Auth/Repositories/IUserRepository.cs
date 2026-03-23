using BankingWallet.Domain.Auth.Entities;

namespace BankingWallet.Domain.Auth.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
}