using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankHeadQuarters.Models
{
    public class AccountStatement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual BankAccount? Account { get; set; }

        public DateTime StatementPeriodStart { get; set; }
        public DateTime StatementPeriodEnd { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OpeningBalance { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ClosingBalance { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalDeposits { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalWithdrawals { get; set; }

        public int TransactionCount { get; set; }

        [StringLength(200)]
        public string? FilePath { get; set; }

        public DateTime GeneratedDate { get; set; }

        public StatementFormat Format { get; set; }

        public bool IsDownloaded { get; set; }
    }

    public enum StatementFormat
    {
        PDF,
        CSV,
        Excel
    }
}
