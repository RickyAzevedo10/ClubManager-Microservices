using AutoMapper;
using Financial.App.Services.Infrastructures;
using Financial.Application.Interfaces.Infrastructure;
using Financial.Domain.DTOs;
using Financial.Domain.Entities;
using Financial.Domain.Interfaces;
using Financial.Domain.Interfaces.Identity;
using Financial.Domain.Interfaces.Repositories;
using static Financial.Domain.Constants.Constants;

namespace Financial.Application.Services
{
    public class ExpenseAppService : IExpenseAppService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExpenseService _expenseService;
        private readonly IMapper _mapper;
        private readonly IUserPermissionsCachedRepository _userPermissionsCachedRepository;

        public ExpenseAppService(INotificationContext notificationContext, IUnitOfWork unitOfWork, IExpenseService expenseService, IMapper mapper, IUserPermissionsCachedRepository userPermissionsCachedRepository)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _expenseService = expenseService;
            _mapper = mapper;
            _userPermissionsCachedRepository = userPermissionsCachedRepository;
        }

        public async Task<ExpenseResponseDTO?> CreateExpense(ExpenseDTO expenseBody)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Create)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CREATE, string.Empty);
                return null;
            }

            Expense? expense = await _expenseService.CreateExpense(expenseBody);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<ExpenseResponseDTO>(expense);
        }

        public async Task<ExpenseResponseDTO?> DeleteExpense(long id)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Delete)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_DELETE, string.Empty);
                return null;
            }

            Expense? facilityDeleted = await _expenseService.DeleteExpense(id);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<ExpenseResponseDTO>(facilityDeleted);
        }

        public async Task<ExpenseResponseDTO> UpdateExpense(UpdateExpenseDTO expenseToUpdate)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Edit)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_EDIT, string.Empty);
                return null;
            }

            Entity? entity = null;

            if (expenseToUpdate.EntityId != null)
                entity = await _unitOfWork.EntityRepository.GetById(expenseToUpdate.EntityId);

            if (entity == null)
            {
                _notificationContext.AddNotification(NotificationKeys.EntityNotifications.ENTITY_DOES_NOT_EXIST, string.Empty);
                return null;
            }

            Expense? expenseUpdated = await _expenseService.UpdateExpense(expenseToUpdate);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<ExpenseResponseDTO>(expenseUpdated);
        }

        public async Task<ExpenseResponseDTO?> GetExpense(long expenseId)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            Expense? expense = await _unitOfWork.ExpenseRepository.GetById(expenseId);

            return _mapper.Map<ExpenseResponseDTO>(expense);
        }

        public async Task<List<ExpenseResponseDTO>?> GetAllExpense()
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            IEnumerable<Expense>? allExpenses = await _unitOfWork.ExpenseRepository.GetAllAsync();

            return _mapper.Map<List<ExpenseResponseDTO>>(allExpenses);
        }

    }
}
