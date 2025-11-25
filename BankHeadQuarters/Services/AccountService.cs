using BankHeadQuarters.Data;
using BankHeadQuarters.Models;
using Microsoft.EntityFrameworkCore;

namespace BankHeadQuarters.Services
{
    public interface IAccountService
    {
        Task<BankAccount?> GetAccountByNumberAsync(string accountNumber);
        Task<List<BankAccount>> GetUserAccountsAsync(string userId);
        Task<BankAccount> CreateAccountAsync(BankAccount account);
        Task<bool> UpdateBalanceAsync(int accountId, decimal amount);
        Task<bool> FreezeAccountAsync(int accountId);
        Task<bool> CloseAccountAsync(int accountId);
    }

    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BankAccount?> GetAccountByNumberAsync(string accountNumber)
        {
            return await _context.BankAccounts
                .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }

        public async Task<List<BankAccount>> GetUserAccountsAsync(string userId)
        {
            return await _context.BankAccounts
                .Where(a => a.UserId == userId && a.Status == AccountStatus.Active)
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync();
        }

        public async Task<BankAccount> CreateAccountAsync(BankAccount account)
        {
            account.AccountNumber = GenerateAccountNumber();
            account.CreatedDate = DateTime.UtcNow;
            account.Status = AccountStatus.Active;

            _context.BankAccounts.Add(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<bool> UpdateBalanceAsync(int accountId, decimal amount)
        {
            var account = await _context.BankAccounts.FindAsync(accountId);
            if (account == null) return false;

            account.Balance += amount;
            account.LastModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> FreezeAccountAsync(int accountId)
        {
            var account = await _context.BankAccounts.FindAsync(accountId);
            if (account == null) return false;

            account.Status = AccountStatus.Frozen;
            account.LastModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CloseAccountAsync(int accountId)
        {
            var account = await _context.BankAccounts.FindAsync(accountId);
            if (account == null || account.Balance != 0) return false;

            account.Status = AccountStatus.Closed;
            account.LastModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        private string GenerateAccountNumber()
        {
            return $"ACC{DateTime.UtcNow.Ticks}";
        }
    }
}
