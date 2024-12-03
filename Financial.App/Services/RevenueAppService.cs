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
    public class RevenueAppService : IRevenueAppService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRevenueService _revenueService;
        private readonly IMapper _mapper;
        private readonly IUserPermissionsCachedRepository _userPermissionsCachedRepository;

        public RevenueAppService(INotificationContext notificationContext, IUnitOfWork unitOfWork, IRevenueService revenueService, IMapper mapper, IUserPermissionsCachedRepository userPermissionsCachedRepository)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _revenueService = revenueService;
            _mapper = mapper;
            _userPermissionsCachedRepository = userPermissionsCachedRepository;
        }

        public async Task<RevenueResponseDTO?> CreateRevenue(RevenueDTO revenueBody)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Create)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CREATE, string.Empty);
                return null;
            }

            Revenue? revenueList = await _revenueService.CreateRevenue(revenueBody);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<RevenueResponseDTO>(revenueList);
        }

        public async Task<RevenueResponseDTO?> DeleteRevenue(long id)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Delete)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_DELETE, string.Empty);
                return null;
            }

            Revenue? revenueDeleted = await _revenueService.DeleteRevenue(id);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<RevenueResponseDTO>(revenueDeleted);
        }

        public async Task<RevenueResponseDTO?> UpdateRevenue(UpdateRevenueDTO revenueToUpdate)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Edit)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_EDIT, string.Empty);
                return null;
            }

            Entity? entity = null;

            if (revenueToUpdate.EntityId != null)
                entity = await _unitOfWork.EntityRepository.GetById(revenueToUpdate.EntityId);

            if (entity == null)
            {
                _notificationContext.AddNotification(NotificationKeys.EntityNotifications.ENTITY_DOES_NOT_EXIST, string.Empty);
                return null;
            }

            Revenue? revenue = await _revenueService.UpdateRevenue(revenueToUpdate);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<RevenueResponseDTO>(revenue);
        }

        public async Task<RevenueResponseDTO?> GetRevenue(long revenueId)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            Revenue? revenue = await _unitOfWork.RevenueRepository.GetById(revenueId);

            return _mapper.Map<RevenueResponseDTO>(revenue);
        }

        public async Task<List<RevenueResponseDTO>?> GetAllRevenue()
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            IEnumerable<Revenue>? allRevenue = await _unitOfWork.RevenueRepository.GetAllAsync();

            return _mapper.Map<List<RevenueResponseDTO>>(allRevenue);
        }
    }
}
