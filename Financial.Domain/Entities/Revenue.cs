using System.ComponentModel.DataAnnotations.Schema;

namespace Financial.Domain.Entities
{
    public class Revenue : BaseEntity
    {
        public DateTime RevenueDate { get; set; }
        public decimal Amount { get; set; }
        public string Source { get; set; }
        public long CategoryId { get; set; }
        public string Description { get; set; }
        public string PaymentReference { get; set; }
        public long EntityId { get; set; }
        public long ResponsibleUserId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual RevenueCategory RevenueCategory { get; set; }
        public virtual Entity Entity { get; set; }
        [ForeignKey("ResponsibleUserId")]
        public virtual User User { get; set; } 

        public void SetRevenueDate(DateTime revenueDate)
        {
            RevenueDate = revenueDate;
        }

        public void SetAmount(decimal amount)
        {
            Amount = amount;
        }

        public void SetSource(string source)
        {
            Source = source;
        }

        public void SetCategoryId(long categoryId)
        {
            CategoryId = categoryId;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetPaymentReference(string paymentReference)
        {
            PaymentReference = paymentReference;
        }

        public void SetEntityId(long entityId)
        {
            EntityId = entityId;
        }

        public void SetResponsibleUserId(long responsibleUserId)
        {
            ResponsibleUserId = responsibleUserId;
        }

    }
}
