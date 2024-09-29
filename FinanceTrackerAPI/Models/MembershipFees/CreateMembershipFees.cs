namespace FinanceTrackerAPI.Models.MembershipFees
{
    public class CreateMembershipFees
    {
        public required string Name { get; set; }
        public required Guid CategoryId { get; set; }
        public required decimal Amount { get; set; }
        public string? Notes { get; set; }
        public bool IsRecurrent { get; set; } = false;
        public string? RecurrencePeriod { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
