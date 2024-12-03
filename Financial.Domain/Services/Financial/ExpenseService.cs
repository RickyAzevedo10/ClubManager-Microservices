using Financial.Domain.Interfaces;
using Financial.Domain.Interfaces.Identity;
using Financial.Domain.Interfaces.Repositories;
using Financial.Domain.DTOs;
using Financial.Domain.Entities;
using FluentValidation;
using static Financial.Domain.Constants.Constants;

namespace Financial.Domain.Services.Identity
{
    public class ExpenseService : IExpenseService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityValidationService _entityValidationService;
        private readonly IValidator<ExpenseDTO> _expenseValidator;

        public ExpenseService(INotificationContext notificationContext, IUnitOfWork unitOfWork, IEntityValidationService entityValidationService, IValidator<ExpenseDTO> expenseValidator)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _entityValidationService = entityValidationService;
            _expenseValidator = expenseValidator;
        }

        public async Task<Expense?> DeleteExpense(long id)
        {
            Expense? expense = await _unitOfWork.ExpenseRepository.GetById(id);

            if (expense == null)
            {
                _notificationContext.AddNotification(NotificationKeys.ExpenseNotifications.EXPENSE_DOES_NOT_EXIST, string.Empty);
                return null;
            }

            _unitOfWork.ExpenseRepository.Delete(expense);
            return expense;
        }

        public async Task<Expense> CreateExpense(ExpenseDTO expenseBody)
        {   
            bool validationResult = _entityValidationService.Validate(_expenseValidator, expenseBody);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.ExpenseNotifications.ERROR_EXPENSE_CREATED, string.Empty);
                return null;
            }
            Expense expense = new();
            expense.SetAmount(expenseBody.Amount);
            expense.SetCategoryId(expenseBody.CategoryId);
            expense.SetDescription(expenseBody.Description);
            expense.SetDestination(expenseBody.Destination);
            expense.SetPaymentReference(expenseBody.PaymentReference);
            expense.SetExpenseDate(expenseBody.ExpenseDate);
            expense.SetResponsibleUserId(expenseBody.ResponsibleUserId);
            expense.SetEntityId(expenseBody.EntityId);

            await _unitOfWork.ExpenseRepository.AddAsync(expense);
            return expense;
        }

        public async Task<Expense> UpdateExpense(UpdateExpenseDTO expenseBody)
        {
            Expense expense = await _unitOfWork.ExpenseRepository.GetById(expenseBody.Id);
            ExpenseDTO expenseDTO = new()
            {
                ExpenseDate = expenseBody.ExpenseDate,
                Amount = expenseBody.Amount,
                Description = expenseBody.Description,
                CategoryId = expenseBody.CategoryId,
                Destination = expenseBody.Destination,
                PaymentReference = expenseBody.PaymentReference,
                EntityId = expenseBody.EntityId,
                ResponsibleUserId = expenseBody.ResponsibleUserId
            };
            bool validationResult = _entityValidationService.Validate(_expenseValidator, expenseDTO);

            if (!validationResult && expense == null)
            {
                _notificationContext.AddNotification(NotificationKeys.ExpenseNotifications.ERROR_EXPENSE_UPDATED, string.Empty);
                return null;
            }

            expense.SetAmount(expenseBody.Amount);
            expense.SetCategoryId(expenseBody.CategoryId);
            expense.SetDescription(expenseBody.Description);
            expense.SetDestination(expenseBody.Destination);
            expense.SetPaymentReference(expenseBody.PaymentReference);
            expense.SetExpenseDate(expenseBody.ExpenseDate);
            expense.SetResponsibleUserId(expenseBody.ResponsibleUserId);
            expense.SetEntityId(expenseBody.EntityId);

            _unitOfWork.ExpenseRepository.Update(expense);

            return expense;
        }

    }
}
