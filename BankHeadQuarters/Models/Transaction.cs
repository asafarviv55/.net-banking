using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankHeadQuarters.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TransactionId { get; set; } = string.Empty;

        [Required]
        public int AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual BankAccount? Account { get; set; }

        [Required]
        public TransactionType Type { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BalanceAfter { get; set; }

        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        [StringLength(50)]
        public string? ReferenceNumber { get; set; }

        public TransactionStatus Status { get; set; }

        public DateTime TransactionDate { get; set; }

        [StringLength(20)]
        public string? RecipientAccountNumber { get; set; }
    }

    public enum TransactionType
    {
        Deposit,
        Withdrawal,
        Transfer,
        Payment,
        Fee,
        Interest,
        Refund
    }

    public enum TransactionStatus
    {
        Pending,
        Completed,
        Failed,
        Cancelled
    }
}
