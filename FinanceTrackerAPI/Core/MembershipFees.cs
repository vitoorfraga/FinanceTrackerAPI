using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTrackerAPI.Core
{
    public class MembershipFees
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        public required string Name { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Amount { get; set; }
        public string? Notes { get; set; }
        public bool IsRecurrent { get; set; }
        public string? RecurrencePeriod { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        // Relacionamentos
        public required Categories Category { get; set; }
    }
}
