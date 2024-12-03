using Financial.Domain.DTOs;
using Financial.Domain.Entities;

namespace Financial.Domain.Interfaces.Identity
{
    public interface IExpenseService
    {
        Task<Expense?> DeleteExpense(long id);
        Task<Expense> CreateExpense(ExpenseDTO expenseBody);
        Task<Expense> UpdateExpense(UpdateExpenseDTO expenseBody);
    }
}