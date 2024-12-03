using System.ComponentModel.DataAnnotations.Schema;

namespace Financial.Domain.Entities
{
    public class Expense : BaseEntity
    {
        public DateTime ExpenseDate { get; set; }
        public decimal Amount { get; set; }
        public string Destination { get; set; }
        public long CategoryId { get; set; }
        public string Description { get; set; }
        public string PaymentReference { get; set; }
        public long EntityId { get; set; }
        public long ResponsibleUserId { get; set; }

        // Propriedades de navegação
        [ForeignKey("CategoryId")]
        public virtual ExpenseCategory ExpenseCategory { get; set; }
        public virtual Entity Entity { get; set; }
        [ForeignKey("ResponsibleUserId")]
        public virtual User User { get; set; }  

        public void SetExpenseDate(DateTime expenseDate)
        {
            ExpenseDate = expenseDate;
        }

        public void SetAmount(decimal amount)
        {
            Amount = amount;
        }

        public void SetDestination(string destination)
        {
            Destination = destination;
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
