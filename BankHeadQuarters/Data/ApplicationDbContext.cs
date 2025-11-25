using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BankHeadQuarters.Models;

namespace BankHeadQuarters.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<BillPayment> BillPayments { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<StandingOrder> StandingOrders { get; set; }
        public DbSet<FixedDeposit> FixedDeposits { get; set; }
        public DbSet<DebitCard> DebitCards { get; set; }
        public DbSet<AccountStatement> AccountStatements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BankAccount>()
                .HasIndex(a => a.AccountNumber)
                .IsUnique();

            modelBuilder.Entity<Transaction>()
                .HasIndex(t => t.TransactionId)
                .IsUnique();

            modelBuilder.Entity<CreditCard>()
                .HasIndex(c => c.CardNumber)
                .IsUnique();

            modelBuilder.Entity<DebitCard>()
                .HasIndex(d => d.CardNumber)
                .IsUnique();

            modelBuilder.Entity<Loan>()
                .HasIndex(l => l.LoanNumber)
                .IsUnique();

            modelBuilder.Entity<FixedDeposit>()
                .HasIndex(f => f.DepositNumber)
                .IsUnique();
        }
    }
}