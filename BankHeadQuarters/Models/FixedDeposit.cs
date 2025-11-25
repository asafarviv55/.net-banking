using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankHeadQuarters.Models
{
    public class FixedDeposit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string DepositNumber { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int LinkedAccountId { get; set; }

        [ForeignKey("LinkedAccountId")]
        public virtual BankAccount? LinkedAccount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrincipalAmount { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal InterestRate { get; set; }

        public int TermMonths { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MaturityAmount { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime MaturityDate { get; set; }

        public FixedDepositStatus Status { get; set; }

        public InterestPayoutFrequency InterestPayoutFrequency { get; set; }

        public bool AutoRenew { get; set; }
    }

    public enum FixedDepositStatus
    {
        Active,
        Matured,
        PrematurelyClosed,
        Renewed
    }

    public enum InterestPayoutFrequency
    {
        Monthly,
        Quarterly,
        HalfYearly,
        Yearly,
        OnMaturity
    }
}
