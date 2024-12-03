using AutoMapper;
using Infrastructures.Application.Interfaces;
using Infrastructures.Application.Interfaces.Infrastructure;
using Infrastructures.Domain.DTOs;
using Infrastructures.Domain.Entities;
using Infrastructures.Domain.Interfaces;
using Infrastructures.Domain.Interfaces.Infrastructures;
using Infrastructures.Domain.Interfaces.Repositories;
using static Infrastructures.Domain.Constants.Constants;

namespace Infrastructures.Application.Services
{
    public class MaintenanceAppService : IMaintenanceAppService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMaintenanceService _maintenanceService;
        private readonly IMapper _mapper;
        private readonly IUserPermissionsCachedRepository _userPermissionsCachedRepository; 

        public MaintenanceAppService(INotificationContext notificationContext, IUnitOfWork unitOfWork, IUserPermissionsCachedRepository userPermissionsCachedRepository,
            IMaintenanceService maintenanceService, IMapper mapper)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _maintenanceService = maintenanceService;
            _userPermissionsCachedRepository = userPermissionsCachedRepository;
            _mapper = mapper;
        }

        #region MaintenanceRequest
        public async Task<MaintenanceRequestResponseDTO?> GetMaintenanceRequest(long maintenanceRequestId)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            MaintenanceRequest? maintenanceRequest = await _unitOfWork.MaintenanceRequestRepository.GetById(maintenanceRequestId);

            return _mapper.Map<MaintenanceRequestResponseDTO>(maintenanceRequest);
        }

        public async Task<MaintenanceRequestResponseDTO?> DeleteMaintenanceRequest(long id)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Delete)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_DELETE, string.Empty);
                return null;
            }

            MaintenanceRequest? maintenanceRequestDeleted = await _maintenanceService.DeleteMaintenanceRequest(id);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<MaintenanceRequestResponseDTO>(maintenanceRequestDeleted);
        }

        public async Task<MaintenanceRequestResponseDTO?> CreateMaintenanceRequest(CreateMaintenanceRequestDTO maintenanceRequestBody)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Create)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CREATE, string.Empty);
                return null;
            }

            MaintenanceRequest? maintenanceRequest = await _maintenanceService.CreateMaintenanceRequest(maintenanceRequestBody);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<MaintenanceRequestResponseDTO>(maintenanceRequest);
        }

        public async Task<MaintenanceRequestResponseDTO?> UpdateMaintenanceRequest(UpdateMaintenanceRequestDTO maintenanceRequestToUpdate)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Edit)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_EDIT, string.Empty);
                return null;
            }

            MaintenanceRequest? maintenanceRequest = null;

            if (maintenanceRequestToUpdate.Id != null)
                maintenanceRequest = await _unitOfWork.MaintenanceRequestRepository.GetById(maintenanceRequestToUpdate.Id);

            if (maintenanceRequest == null)
            {
                _notificationContext.AddNotification(NotificationKeys.FacilityNotifications.FACILITY_DOES_NOT_EXIST, string.Empty);
                return null;
            }

            maintenanceRequest = await _maintenanceService.UpdateMaintenanceRequest(maintenanceRequestToUpdate, maintenanceRequest);

            if (maintenanceRequest == null)
            {
                _notificationContext.AddNotification(NotificationKeys.FacilityNotifications.ERROR_FACILITY_UPDATED, string.Empty);
                return null;
            }

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<MaintenanceRequestResponseDTO>(maintenanceRequest);
        }
        #endregion

        #region MaintenanceHistory
        public async Task<MaintenanceHistory?> CreateMaintenanceHistory(long maintenanceRequestId)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Create)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CREATE, string.Empty);
                return null;
            }

            MaintenanceHistory? maintenanceHistory = await _maintenanceService.CreateMaintenanceHistory(maintenanceRequestId);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return maintenanceHistory;
        }

        public async Task<List<MaintenanceHistory>?> GetMaintenanceHistory(DateTime startDate, DateTime endDate)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            if (startDate > endDate)
            {
                _notificationContext.AddNotification(NotificationKeys.MaintenanceHistoryNotifications.MAINTENANCE_HISTORY_DATETIME_INVALID, string.Empty);
                return null;
            }

            List<MaintenanceHistory>? listMaintenanceHistory = await _unitOfWork.MaintenanceHistoryRepository.GetMaintenanceHistoryByDateRange(startDate, endDate);

            return listMaintenanceHistory;
        }

        #endregion
    }
}
