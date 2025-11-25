using System.ComponentModel.DataAnnotations;

namespace BankHeadQuarters.Models
{
    public class Beneficiary
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string BeneficiaryName { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string AccountNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string BankName { get; set; } = string.Empty;

        [StringLength(20)]
        public string? RoutingNumber { get; set; }

        [StringLength(50)]
        public string? IBAN { get; set; }

        [StringLength(20)]
        public string? SWIFT { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        public BeneficiaryType Type { get; set; }

        public bool IsVerified { get; set; }

        public DateTime DateAdded { get; set; }
        public DateTime? LastUsedDate { get; set; }
    }

    public enum BeneficiaryType
    {
        Individual,
        Business,
        International
    }
}
