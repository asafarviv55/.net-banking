using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankHeadQuarters.Models
{
    public class CreditCard
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(16)]
        public string CardNumber { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string CardHolderName { get; set; } = string.Empty;

        [Required]
        public CardType CardType { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CreditLimit { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentBalance { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AvailableCredit { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal InterestRate { get; set; }

        public DateTime ExpirationDate { get; set; }

        [StringLength(3)]
        public string CVV { get; set; } = string.Empty;

        public CardStatus Status { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime? LastUsedDate { get; set; }
    }

    public enum CardType
    {
        Visa,
        MasterCard,
        AmericanExpress,
        Discover
    }

    public enum CardStatus
    {
        Active,
        Blocked,
        Expired,
        Cancelled
    }
}
