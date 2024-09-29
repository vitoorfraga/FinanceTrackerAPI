namespace FinanceTrackerAPI.Models.MembershipFees
{
    public class UpdateMembershipFeesById
    {
        public string? Name { get; set; }
        public decimal? Amount { get; set; }
        public string? Notes { get; set; }
        public bool IsRecurrent { get; set; } = false;
        public string? RecurrencePeriod { get; set; }
    }
}
