namespace Financial.Domain.DTOs
{
    public class RevenueDTO
    {
        public DateTime RevenueDate { get; set; }
        public decimal Amount { get; set; }
        public string Source { get; set; }
        public long CategoryId { get; set; }
        public string Description { get; set; }
        public string PaymentReference { get; set; }
        public long EntityId { get; set; }
        public long ResponsibleUserId { get; set; }
    }
}
