using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankHeadQuarters.Models
{
    public class StandingOrder
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual BankAccount? Account { get; set; }

        [Required]
        [StringLength(100)]
        public string RecipientName { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string RecipientAccountNumber { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public StandingOrderFrequency Frequency { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime NextExecutionDate { get; set; }
        public DateTime? LastExecutionDate { get; set; }

        [StringLength(200)]
        public string Reference { get; set; } = string.Empty;

        public StandingOrderStatus Status { get; set; }

        public int ExecutionCount { get; set; }
    }

    public enum StandingOrderFrequency
    {
        Daily,
        Weekly,
        BiWeekly,
        Monthly,
        Quarterly,
        SemiAnnually,
        Annually
    }

    public enum StandingOrderStatus
    {
        Active,
        Paused,
        Cancelled,
        Completed
    }
}
