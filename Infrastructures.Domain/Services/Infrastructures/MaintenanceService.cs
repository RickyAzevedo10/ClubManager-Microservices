using Infrastructures.Domain.Interfaces;
using Infrastructures.Domain.Interfaces.Infrastructures;
using Infrastructures.Domain.Interfaces.Repositories;
using FluentValidation;
using static Infrastructures.Domain.Constants.Constants;
using Infrastructures.Domain.DTOs;
using Infrastructures.Domain.Entities;

namespace Infrastructures.Domain.Services.Infrastructures
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityValidationService _entityValidationService;
        private readonly IValidator<CreateMaintenanceRequestDTO> _createMaintenanceRequestValidator;

        public MaintenanceService(INotificationContext notificationContext, IUnitOfWork unitOfWork,
            IEntityValidationService entityValidationService, IValidator<CreateMaintenanceRequestDTO> createMaintenanceRequestValidator)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _entityValidationService = entityValidationService;
            _createMaintenanceRequestValidator = createMaintenanceRequestValidator;
        }

        #region MaintenanceRequest
        public async Task<MaintenanceRequest?> DeleteMaintenanceRequest(long id)
        {
            MaintenanceRequest? maintenanceRequest = await _unitOfWork.MaintenanceRequestRepository.GetById(id);

            if (maintenanceRequest == null)
            {
                _notificationContext.AddNotification(NotificationKeys.MaintenanceRequestNotifications.MAINTENANCE_REQUEST_DOES_NOT_EXIST, string.Empty);
                return null;
            }

            _unitOfWork.MaintenanceRequestRepository.Delete(maintenanceRequest);
            return maintenanceRequest;
        }

        public async Task<MaintenanceRequest?> CreateMaintenanceRequest(CreateMaintenanceRequestDTO maintenanceRequestBody)
        {
            bool validationResult = _entityValidationService.Validate(_createMaintenanceRequestValidator, maintenanceRequestBody);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.MaintenanceRequestNotifications.ERROR_MAINTENANCE_REQUEST_CREATED, string.Empty);
                return null;
            }

            MaintenanceRequest maintenanceRequest = new();
            maintenanceRequest.SetMaintenanceType(maintenanceRequestBody.MaintenanceType);
            maintenanceRequest.SetStatus(maintenanceRequestBody.Status);
            maintenanceRequest.SetRequestDate(maintenanceRequestBody.RequestDate);
            maintenanceRequest.SetPriority(maintenanceRequestBody.Priority);
            maintenanceRequest.SetFacilityId(maintenanceRequestBody.FacilityId);
            maintenanceRequest.SetProblemDescription(maintenanceRequestBody.ProblemDescription);
            maintenanceRequest.SetRequestedUserId(maintenanceRequestBody.RequestedUserId);

            return await _unitOfWork.MaintenanceRequestRepository.AddAsync(maintenanceRequest);
        }

        public async Task<MaintenanceRequest?> UpdateMaintenanceRequest(UpdateMaintenanceRequestDTO maintenanceRequestToUpdate, MaintenanceRequest maintenanceRequest)
        {
            CreateMaintenanceRequestDTO maintenanceRequestDTO = new()
            {
                FacilityId = (int)maintenanceRequest.FacilityId,
                MaintenanceType = maintenanceRequestToUpdate.MaintenanceType,
                ProblemDescription = maintenanceRequestToUpdate.ProblemDescription,
                Priority = maintenanceRequestToUpdate.Priority,
                RequestDate = maintenanceRequestToUpdate.RequestDate,
                RequestedUserId = maintenanceRequestToUpdate.RequestedUserId,
                Status = maintenanceRequestToUpdate.Status
            };

            bool validationResult = _entityValidationService.Validate(_createMaintenanceRequestValidator, maintenanceRequestDTO);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.MaintenanceRequestNotifications.ERROR_MAINTENANCE_REQUEST_UPDATED, string.Empty);
                return null;
            }

            maintenanceRequest.SetMaintenanceType(maintenanceRequestDTO.MaintenanceType);
            maintenanceRequest.SetStatus(maintenanceRequestDTO.Status);
            maintenanceRequest.SetRequestDate(maintenanceRequestDTO.RequestDate);
            maintenanceRequest.SetPriority(maintenanceRequestDTO.Priority);
            maintenanceRequest.SetFacilityId(maintenanceRequestDTO.FacilityId);
            maintenanceRequest.SetProblemDescription(maintenanceRequestDTO.ProblemDescription);
            maintenanceRequest.SetRequestedUserId(maintenanceRequestDTO.RequestedUserId);

            _unitOfWork.MaintenanceRequestRepository.Update(maintenanceRequest);
            return maintenanceRequest;
        }
        #endregion

        #region MaintenanceHistory
        public async Task<MaintenanceHistory?> CreateMaintenanceHistory(long maintenanceRequestId)
        {
            MaintenanceRequest maintenanceRequest = await _unitOfWork.MaintenanceRequestRepository.GetById(maintenanceRequestId);
            if (maintenanceRequest == null)
            {
                _notificationContext.AddNotification(NotificationKeys.MaintenanceRequestNotifications.MAINTENANCE_REQUEST_DOES_NOT_EXIST, string.Empty);
                return null;
            }

            MaintenanceHistory maintenanceHistory = new();
            maintenanceHistory.SetMaintenanceType(maintenanceRequest.MaintenanceType);
            maintenanceHistory.SetFacilityId(maintenanceRequest.FacilityId);
            maintenanceHistory.SetDescription(maintenanceRequest.ProblemDescription);
            maintenanceHistory.SetMaintenanceDate(maintenanceRequest.RequestDate);
            maintenanceHistory.SetRequestUserId(maintenanceRequest.RequestedUserId);

            return await _unitOfWork.MaintenanceHistoryRepository.AddAsync(maintenanceHistory);
        }


        #endregion
    }
}
