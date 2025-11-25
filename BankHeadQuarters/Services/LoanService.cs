using BankHeadQuarters.Data;
using BankHeadQuarters.Models;
using Microsoft.EntityFrameworkCore;

namespace BankHeadQuarters.Services
{
    public interface ILoanService
    {
        Task<Loan> CreateLoanAsync(Loan loan);
        Task<List<Loan>> GetUserLoansAsync(string userId);
        Task<bool> ApproveLoanAsync(int loanId);
        Task<bool> ProcessPaymentAsync(int loanId, decimal amount);
        Task<decimal> CalculateMonthlyPayment(decimal principal, decimal annualRate, int months);
    }

    public class LoanService : ILoanService
    {
        private readonly ApplicationDbContext _context;

        public LoanService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Loan> CreateLoanAsync(Loan loan)
        {
            loan.LoanNumber = GenerateLoanNumber();
            loan.Status = LoanStatus.Pending;
            loan.StartDate = DateTime.UtcNow;
            loan.OutstandingBalance = loan.PrincipalAmount;
            loan.PaymentsRemaining = loan.TermMonths;
            loan.PaymentsMade = 0;
            loan.MaturityDate = loan.StartDate.AddMonths(loan.TermMonths);
            loan.NextPaymentDate = loan.StartDate.AddMonths(1);

            loan.MonthlyPayment = await CalculateMonthlyPayment(
                loan.PrincipalAmount,
                loan.InterestRate,
                loan.TermMonths
            );

            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            return loan;
        }

        public async Task<List<Loan>> GetUserLoansAsync(string userId)
        {
            return await _context.Loans
                .Where(l => l.UserId == userId)
                .OrderByDescending(l => l.StartDate)
                .ToListAsync();
        }

        public async Task<bool> ApproveLoanAsync(int loanId)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null || loan.Status != LoanStatus.Pending) return false;

            loan.Status = LoanStatus.Active;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ProcessPaymentAsync(int loanId, decimal amount)
        {
            var loan = await _context.Loans.FindAsync(loanId);
            if (loan == null || loan.Status != LoanStatus.Active || amount <= 0)
                return false;

            loan.OutstandingBalance -= amount;
            loan.PaymentsMade++;
            loan.PaymentsRemaining--;
            loan.NextPaymentDate = loan.NextPaymentDate?.AddMonths(1);

            if (loan.OutstandingBalance <= 0)
            {
                loan.Status = LoanStatus.PaidOff;
                loan.OutstandingBalance = 0;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> CalculateMonthlyPayment(decimal principal, decimal annualRate, int months)
        {
            if (annualRate == 0) return principal / months;

            var monthlyRate = annualRate / 100 / 12;
            var payment = principal * monthlyRate * (decimal)Math.Pow((double)(1 + monthlyRate), months) /
                         ((decimal)Math.Pow((double)(1 + monthlyRate), months) - 1);

            return Math.Round(payment, 2);
        }

        private string GenerateLoanNumber()
        {
            return $"LOAN{DateTime.UtcNow.Ticks}";
        }
    }
}
