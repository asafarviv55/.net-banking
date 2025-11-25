using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankHeadQuarters.Models
{
    public class BillPayment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual BankAccount? Account { get; set; }

        [Required]
        [StringLength(100)]
        public string PayeeName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string PayeeAccountNumber { get; set; } = string.Empty;

        [Required]
        public BillCategory Category { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime DueDate { get; set; }
        public DateTime? PaymentDate { get; set; }

        public PaymentStatus Status { get; set; }

        public bool IsRecurring { get; set; }
        public RecurrenceFrequency? RecurrenceFrequency { get; set; }

        [StringLength(200)]
        public string? Notes { get; set; }

        [StringLength(50)]
        public string? ConfirmationNumber { get; set; }
    }

    public enum BillCategory
    {
        Utilities,
        Rent,
        Mortgage,
        Insurance,
        CreditCard,
        Internet,
        Phone,
        Other
    }

    public enum PaymentStatus
    {
        Scheduled,
        Processing,
        Completed,
        Failed,
        Cancelled
    }

    public enum RecurrenceFrequency
    {
        Weekly,
        BiWeekly,
        Monthly,
        Quarterly,
        Annually
    }
}
