using Financial.Domain.Interfaces;
using Financial.Domain.Interfaces.Identity;
using Financial.Domain.Interfaces.Repositories;
using Financial.Domain.DTOs;
using Financial.Domain.Entities;
using FluentValidation;
using static Financial.Domain.Constants.Constants;

namespace Financial.Domain.Services.Identity
{
    public class RevenueService : IRevenueService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityValidationService _entityValidationService;
        private readonly IValidator<RevenueDTO> _revenueValidator;

        public RevenueService(INotificationContext notificationContext, IUnitOfWork unitOfWork, IEntityValidationService entityValidationService, IValidator<RevenueDTO> revenueValidator)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _entityValidationService = entityValidationService;
            _revenueValidator = revenueValidator;
        }

        public async Task<Revenue?> CreateRevenue(RevenueDTO revenueBody)
        {
            bool validationResult = _entityValidationService.Validate(_revenueValidator, revenueBody);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.ExpenseNotifications.ERROR_EXPENSE_CREATED, string.Empty);
                return null;
            }
            Revenue revenue = new();
            revenue.SetAmount(revenueBody.Amount);
            revenue.SetRevenueDate(revenueBody.RevenueDate);
            revenue.SetSource(revenueBody.Source);
            revenue.SetCategoryId(revenueBody.CategoryId);
            revenue.SetDescription(revenueBody.Description);
            revenue.SetPaymentReference(revenueBody.PaymentReference);
            revenue.SetEntityId(revenueBody.EntityId);
            revenue.SetResponsibleUserId(revenueBody.ResponsibleUserId);

            await _unitOfWork.RevenueRepository.AddAsync(revenue);

            return revenue;
        }

        public async Task<Revenue?> UpdateRevenue(UpdateRevenueDTO revenueBody)
        {
            Revenue revenue = await _unitOfWork.RevenueRepository.GetById(revenueBody.Id);
            RevenueDTO revenueDTO = new()
            {
                RevenueDate = revenueBody.RevenueDate,
                Source = revenueBody.Source,
                Amount = revenueBody.Amount,
                Description = revenueBody.Description,
                CategoryId = revenueBody.CategoryId,
                PaymentReference = revenueBody.PaymentReference,
                EntityId = revenueBody.EntityId,
                ResponsibleUserId = revenueBody.ResponsibleUserId
            };

            bool validationResult = _entityValidationService.Validate(_revenueValidator, revenueDTO);

            if (!validationResult && revenue == null)
            {
                _notificationContext.AddNotification(NotificationKeys.ExpenseNotifications.ERROR_EXPENSE_UPDATED, string.Empty);
                return null;
            }

            revenue.SetAmount(revenueBody.Amount);
            revenue.SetRevenueDate(revenueBody.RevenueDate);
            revenue.SetSource(revenueBody.Source);
            revenue.SetCategoryId(revenueBody.CategoryId);
            revenue.SetDescription(revenueBody.Description);
            revenue.SetPaymentReference(revenueBody.PaymentReference);
            revenue.SetEntityId(revenueBody.EntityId);

            _unitOfWork.RevenueRepository.Update(revenue);
              
            return revenue;
        }

        public async Task<Revenue?> DeleteRevenue(long id)
        {
            Revenue? revenue = await _unitOfWork.RevenueRepository.GetById(id);

            if (revenue == null)
            {
                _notificationContext.AddNotification(NotificationKeys.RevenueNotifications.REVENUE_DOES_NOT_EXIST, string.Empty);
                return null;
            }

            _unitOfWork.RevenueRepository.Delete(revenue);
            return revenue;
        }
    }
}
