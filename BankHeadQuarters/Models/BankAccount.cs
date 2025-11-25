using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankHeadQuarters.Models
{
    public class BankAccount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string AccountNumber { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string AccountHolderName { get; set; } = string.Empty;

        [Required]
        public AccountType AccountType { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal InterestRate { get; set; }

        public AccountStatus Status { get; set; }

        [Required]
        public string Currency { get; set; } = "USD";

        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }

    public enum AccountType
    {
        Checking,
        Savings,
        Business,
        Student
    }

    public enum AccountStatus
    {
        Active,
        Inactive,
        Frozen,
        Closed
    }
}
