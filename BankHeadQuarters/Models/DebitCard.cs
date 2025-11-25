using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankHeadQuarters.Models
{
    public class DebitCard
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(16)]
        public string CardNumber { get; set; } = string.Empty;

        [Required]
        public int AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual BankAccount? Account { get; set; }

        [Required]
        [StringLength(100)]
        public string CardHolderName { get; set; } = string.Empty;

        [Required]
        public DebitCardType CardType { get; set; }

        public DateTime ExpirationDate { get; set; }

        [StringLength(3)]
        public string CVV { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal DailyWithdrawalLimit { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DailyPurchaseLimit { get; set; }

        public DebitCardStatus Status { get; set; }

        public bool IsContactless { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime? LastUsedDate { get; set; }

        [StringLength(4)]
        public string PIN { get; set; } = string.Empty;
    }

    public enum DebitCardType
    {
        Visa,
        MasterCard,
        Maestro
    }

    public enum DebitCardStatus
    {
        Active,
        Blocked,
        Expired,
        Lost,
        Stolen,
        Cancelled
    }
}
