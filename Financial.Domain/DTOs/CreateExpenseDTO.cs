namespace Financial.Domain.DTOs
{
    public class ExpenseDTO
    {
        public DateTime ExpenseDate { get; set; }
        public decimal Amount { get; set; }
        public string Destination { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string PaymentReference { get; set; }
        public long EntityId { get; set; }
        public long ResponsibleUserId { get; set; }
    }
}
