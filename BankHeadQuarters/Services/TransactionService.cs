using BankHeadQuarters.Data;
using BankHeadQuarters.Models;
using Microsoft.EntityFrameworkCore;

namespace BankHeadQuarters.Services
{
    public interface ITransactionService
    {
        Task<Transaction> CreateTransactionAsync(Transaction transaction);
        Task<List<Transaction>> GetAccountTransactionsAsync(int accountId, int pageSize = 50);
        Task<Transaction?> GetTransactionByIdAsync(string transactionId);
        Task<bool> ProcessDepositAsync(int accountId, decimal amount, string description);
        Task<bool> ProcessWithdrawalAsync(int accountId, decimal amount, string description);
        Task<bool> ProcessTransferAsync(int fromAccountId, int toAccountId, decimal amount, string description);
    }

    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountService _accountService;

        public TransactionService(ApplicationDbContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
        {
            transaction.TransactionId = GenerateTransactionId();
            transaction.TransactionDate = DateTime.UtcNow;
            transaction.Status = TransactionStatus.Completed;

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<List<Transaction>> GetAccountTransactionsAsync(int accountId, int pageSize = 50)
        {
            return await _context.Transactions
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.TransactionDate)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Transaction?> GetTransactionByIdAsync(string transactionId)
        {
            return await _context.Transactions
                .FirstOrDefaultAsync(t => t.TransactionId == transactionId);
        }

        public async Task<bool> ProcessDepositAsync(int accountId, decimal amount, string description)
        {
            var account = await _context.BankAccounts.FindAsync(accountId);
            if (account == null || amount <= 0) return false;

            account.Balance += amount;
            account.LastModifiedDate = DateTime.UtcNow;

            var transaction = new Transaction
            {
                TransactionId = GenerateTransactionId(),
                AccountId = accountId,
                Type = TransactionType.Deposit,
                Amount = amount,
                BalanceAfter = account.Balance,
                Description = description,
                Status = TransactionStatus.Completed,
                TransactionDate = DateTime.UtcNow
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ProcessWithdrawalAsync(int accountId, decimal amount, string description)
        {
            var account = await _context.BankAccounts.FindAsync(accountId);
            if (account == null || amount <= 0 || account.Balance < amount) return false;

            account.Balance -= amount;
            account.LastModifiedDate = DateTime.UtcNow;

            var transaction = new Transaction
            {
                TransactionId = GenerateTransactionId(),
                AccountId = accountId,
                Type = TransactionType.Withdrawal,
                Amount = amount,
                BalanceAfter = account.Balance,
                Description = description,
                Status = TransactionStatus.Completed,
                TransactionDate = DateTime.UtcNow
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ProcessTransferAsync(int fromAccountId, int toAccountId, decimal amount, string description)
        {
            var fromAccount = await _context.BankAccounts.FindAsync(fromAccountId);
            var toAccount = await _context.BankAccounts.FindAsync(toAccountId);

            if (fromAccount == null || toAccount == null || amount <= 0 || fromAccount.Balance < amount)
                return false;

            fromAccount.Balance -= amount;
            toAccount.Balance += amount;
            fromAccount.LastModifiedDate = DateTime.UtcNow;
            toAccount.LastModifiedDate = DateTime.UtcNow;

            var transactionId = GenerateTransactionId();

            var debitTransaction = new Transaction
            {
                TransactionId = transactionId,
                AccountId = fromAccountId,
                Type = TransactionType.Transfer,
                Amount = -amount,
                BalanceAfter = fromAccount.Balance,
                Description = description,
                RecipientAccountNumber = toAccount.AccountNumber,
                Status = TransactionStatus.Completed,
                TransactionDate = DateTime.UtcNow
            };

            var creditTransaction = new Transaction
            {
                TransactionId = $"{transactionId}-CR",
                AccountId = toAccountId,
                Type = TransactionType.Transfer,
                Amount = amount,
                BalanceAfter = toAccount.Balance,
                Description = description,
                ReferenceNumber = transactionId,
                Status = TransactionStatus.Completed,
                TransactionDate = DateTime.UtcNow
            };

            _context.Transactions.AddRange(debitTransaction, creditTransaction);
            await _context.SaveChangesAsync();

            return true;
        }

        private string GenerateTransactionId()
        {
            return $"TXN{DateTime.UtcNow.Ticks}";
        }
    }
}
