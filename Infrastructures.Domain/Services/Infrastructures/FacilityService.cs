using Infrastructures.Domain.Interfaces;
using Infrastructures.Domain.Interfaces.Infrastructures;
using Infrastructures.Domain.Interfaces.Repositories;
using FluentValidation;
using static Infrastructures.Domain.Constants.Constants;
using Infrastructures.Domain.DTOs;
using Infrastructures.Domain.Entities;

namespace Infrastructures.Domain.Services.Infrastructures
{
    public class FacilityService : IFacilityService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityValidationService _entityValidationService;
        private readonly IValidator<CreateFacilityDTO> _createFacilityValidator;
        private readonly IValidator<CreateFacilityReservationDTO> _createFacilityReservationValidator;

        public FacilityService(INotificationContext notificationContext, IUnitOfWork unitOfWork,
            IEntityValidationService entityValidationService, IValidator<CreateFacilityDTO> createFacilityValidator, IValidator<CreateFacilityReservationDTO> createFacilityReservationValidator)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _entityValidationService = entityValidationService;
            _createFacilityValidator = createFacilityValidator;
            _createFacilityReservationValidator = createFacilityReservationValidator;
        }

        #region Facility
        public async Task<Facility?> CreateFacility(CreateFacilityDTO facilityBody)
        {
            bool validationResult = _entityValidationService.Validate(_createFacilityValidator, facilityBody);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.FacilityNotifications.ERROR_FACILITY_CREATED, string.Empty);
                return null;
            }

            Facility facility = new();
            facility.SetCapacity(facilityBody.Capacity);
            facility.SetDescription(facilityBody.Description);
            facility.SetLocation(facilityBody.Location);
            facility.SetFacilityCategoryId(facilityBody.FacilityCategoryId);
            facility.SetName(facilityBody.Name);

            return await _unitOfWork.FacilityRepository.AddAsync(facility);
        }

        public async Task<Facility?> DeleteFacility(long id)
        {
            Facility? facility = await _unitOfWork.FacilityRepository.GetById(id);

            if (facility == null)
            {
                _notificationContext.AddNotification(NotificationKeys.FacilityNotifications.FACILITY_DOES_NOT_EXIST, string.Empty);
                return null;
            }

            _unitOfWork.FacilityRepository.Delete(facility);
            return facility;
        }

        public async Task<Facility?> UpdateFacility(UpdateFacilityDTO facilityToUpdate, Facility facility)
        {
            CreateFacilityDTO facilityDTO = new()
            {
                Name = facilityToUpdate.Name,
                FacilityCategoryId = facilityToUpdate.FacilityCategoryId,
                Location = facilityToUpdate.Location,
                Capacity = facilityToUpdate.Capacity,
                Description = facilityToUpdate.Description
            };

            bool validationResult = _entityValidationService.Validate(_createFacilityValidator, facilityDTO);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.FacilityNotifications.ERROR_FACILITY_UPDATED, string.Empty);
                return null;
            }

            facility.SetCapacity(facilityDTO.Capacity);
            facility.SetDescription(facilityDTO.Description);
            facility.SetLocation(facilityDTO.Location);
            facility.SetFacilityCategoryId(facilityDTO.FacilityCategoryId);
            facility.SetName(facilityDTO.Name);

            _unitOfWork.FacilityRepository.Update(facility);
            return facility;
        }
        #endregion

        #region FacilityReservation
        public async Task<FacilityReservation?> DeleteFacilityReservation(long id)
        {
            FacilityReservation? facilityReservation = await _unitOfWork.FacilityReservationRepository.GetById(id);

            if (facilityReservation == null)
            {
                _notificationContext.AddNotification(NotificationKeys.FacilityReservationNotifications.FACILITY_RESERVATION_DOES_NOT_EXIST, string.Empty);
                return null;
            }

            _unitOfWork.FacilityReservationRepository.Delete(facilityReservation);
            return facilityReservation;
        }

        public async Task<FacilityReservation?> CreateFacilityReservation(CreateFacilityReservationDTO facilityReservationBody)
        {
            bool validationResult = _entityValidationService.Validate(_createFacilityReservationValidator, facilityReservationBody);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.FacilityReservationNotifications.ERROR_FACILITY_RESERVATION_CREATED, string.Empty);
                return null;
            }

            FacilityReservation facilityReservation = new();
            facilityReservation.SetEndTime(facilityReservationBody.EndTime);
            facilityReservation.SetStartTime(facilityReservationBody.StartTime);
            facilityReservation.SetEventDescription(facilityReservationBody.EventDescription);
            facilityReservation.SetEventType(facilityReservationBody.EventType);
            facilityReservation.SetReservedUserId(facilityReservationBody.ReservedUserId);
            facilityReservation.SetFacilityId(facilityReservationBody.FacilityId);

            return await _unitOfWork.FacilityReservationRepository.AddAsync(facilityReservation);
        }

        public async Task<FacilityReservation?> UpdateFacilityReservation(UpdateFacilityReservationDTO facilityReservationToUpdate, FacilityReservation facilityReservation)
        {
            CreateFacilityReservationDTO facilityReservationDTO = new()
            {
                FacilityId = facilityReservationToUpdate.FacilityId,
                StartTime = facilityReservationToUpdate.StartTime,
                EndTime = facilityReservationToUpdate.EndTime,
                EventDescription = facilityReservationToUpdate.EventDescription,
                EventType = facilityReservationToUpdate.EventType,
                ReservedUserId = facilityReservationToUpdate.ReservedUserId
            };

            bool validationResult = _entityValidationService.Validate(_createFacilityReservationValidator, facilityReservationDTO);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.FacilityReservationNotifications.ERROR_FACILITY_RESERVATION_UPDATED, string.Empty);
                return null;
            }

            facilityReservation.SetEndTime(facilityReservationDTO.EndTime);
            facilityReservation.SetStartTime(facilityReservationDTO.StartTime);
            facilityReservation.SetEventDescription(facilityReservationDTO.EventDescription);
            facilityReservation.SetEventType(facilityReservationDTO.EventType);
            facilityReservation.SetReservedUserId(facilityReservationDTO.ReservedUserId);
            facilityReservation.SetFacilityId(facilityReservationDTO.FacilityId);

            _unitOfWork.FacilityReservationRepository.Update(facilityReservation);
            return facilityReservation;
        }
        #endregion
    }
}
