using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankHeadQuarters.Models
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string LoanNumber { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public LoanType Type { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrincipalAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OutstandingBalance { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal InterestRate { get; set; }

        public int TermMonths { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MonthlyPayment { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public DateTime? NextPaymentDate { get; set; }

        public LoanStatus Status { get; set; }

        public int PaymentsMade { get; set; }
        public int PaymentsRemaining { get; set; }
    }

    public enum LoanType
    {
        Personal,
        Home,
        Auto,
        Business,
        Student,
        LineOfCredit
    }

    public enum LoanStatus
    {
        Pending,
        Approved,
        Active,
        PaidOff,
        Defaulted,
        Rejected
    }
}
