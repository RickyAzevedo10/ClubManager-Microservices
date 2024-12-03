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
    public class FacilityAppService : IFacilityAppService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFacilityService _facilityService;
        private readonly IMapper _mapper;
        private readonly IUserPermissionsCachedRepository _userPermissionsCachedRepository;

        public FacilityAppService(INotificationContext notificationContext, IUnitOfWork unitOfWork, IUserPermissionsCachedRepository userPermissionsCachedRepository,
            IFacilityService facilityService, IMapper mapper)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _userPermissionsCachedRepository = userPermissionsCachedRepository;
            _facilityService = facilityService;
            _mapper = mapper;
        }

        #region Facility
        public async Task<FacilityResponseDTO?> GetFacility(long facilityId)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            Facility? facility = await _unitOfWork.FacilityRepository.GetById(facilityId);

            return _mapper.Map<FacilityResponseDTO>(facility);
        }

        public async Task<List<FacilityResponseDTO>?> GetAllFacility()
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            IEnumerable<Facility>? allFacility = await _unitOfWork.FacilityRepository.GetAllAsync();

            return _mapper.Map<List<FacilityResponseDTO>>(allFacility);
        }

        public async Task<FacilityResponseDTO?> DeleteFacility(long id)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Delete)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_DELETE, string.Empty);
                return null;
            }

            Facility? facilityDeleted = await _facilityService.DeleteFacility(id);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<FacilityResponseDTO>(facilityDeleted);
        }

        public async Task<FacilityResponseDTO?> CreateFacility(CreateFacilityDTO facilityBody)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Create)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CREATE, string.Empty);
                return null;
            }

            Facility? facility = await _facilityService.CreateFacility(facilityBody);


            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<FacilityResponseDTO>(facility);
        }

        public async Task<FacilityResponseDTO?> UpdateFacility(UpdateFacilityDTO facilityToUpdate)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Edit)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_EDIT, string.Empty);
                return null;
            }

            Facility? facility = null;

            if (facilityToUpdate.Id != null)
                facility = await _unitOfWork.FacilityRepository.GetById(facilityToUpdate.Id);

            if (facility == null)
            {
                _notificationContext.AddNotification(NotificationKeys.FacilityNotifications.FACILITY_DOES_NOT_EXIST, string.Empty);
                return null;
            }

            facility = await _facilityService.UpdateFacility(facilityToUpdate, facility);

            if (facility == null)
            {
                _notificationContext.AddNotification(NotificationKeys.FacilityNotifications.ERROR_FACILITY_UPDATED, string.Empty);
                return null;
            }

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<FacilityResponseDTO>(facility);
        }
        #endregion

        #region FacilityReservation
        public async Task<FacilityReservationResponseDTO?> GetFacilityReservation(long facilityReservationId)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            FacilityReservation? facilityReservation = await _unitOfWork.FacilityReservationRepository.GetById(facilityReservationId);

            return _mapper.Map<FacilityReservationResponseDTO>(facilityReservation);
        }

        public async Task<FacilityReservationResponseDTO?> DeleteFacilityReservation(long id)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Delete)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_DELETE, string.Empty);
                return null;
            }

            FacilityReservation? facilityReservationDeleted = await _facilityService.DeleteFacilityReservation(id);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<FacilityReservationResponseDTO>(facilityReservationDeleted);
        }

        public async Task<FacilityReservationResponseDTO?> CreateFacilityReservation(CreateFacilityReservationDTO facilityReservationBody)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Create)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CREATE, string.Empty);
                return null;
            }

            FacilityReservation? facilityReservation = await _facilityService.CreateFacilityReservation(facilityReservationBody);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<FacilityReservationResponseDTO>(facilityReservation);
        }

        public async Task<FacilityReservationResponseDTO?> UpdateFacilityReservation(UpdateFacilityReservationDTO facilityReservationToUpdate)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Edit)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_EDIT, string.Empty);
                return null;
            }

            FacilityReservation? facilityReservation = null;

            if (facilityReservationToUpdate.Id != null)
                facilityReservation = await _unitOfWork.FacilityReservationRepository.GetById(facilityReservationToUpdate.Id);

            if (facilityReservation == null)
            {
                _notificationContext.AddNotification(NotificationKeys.FacilityReservationNotifications.FACILITY_RESERVATION_DOES_NOT_EXIST, string.Empty);
                return null;
            }

            facilityReservation = await _facilityService.UpdateFacilityReservation(facilityReservationToUpdate, facilityReservation);

            if (facilityReservation == null)
            {
                _notificationContext.AddNotification(NotificationKeys.FacilityNotifications.ERROR_FACILITY_UPDATED, string.Empty);
                return null;
            }

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<FacilityReservationResponseDTO>(facilityReservation);
        }
        #endregion
    }
}
