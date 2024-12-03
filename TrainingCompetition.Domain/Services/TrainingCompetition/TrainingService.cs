using TrainingCompetition.Domain.Interfaces;
using TrainingCompetition.Domain.Interfaces.Identity;
using TrainingCompetition.Domain.Interfaces.Repositories;
using FluentValidation;
using TrainingCompetition.Domain.DTOs;
using TrainingCompetition.Domain.Entities;
using static TrainingCompetition.Domain.Constants.Constants;

namespace TrainingCompetition.Domain.Services.Identity
{
    public class TrainingService : ITrainingService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityValidationService _entityValidationService;
        private readonly IValidator<CreateTrainingSessionDTO> _createTrainingSessionValidator;
        private readonly IValidator<CreateTrainingAttendanceDTO> _createTrainingAttendanceValidator;

        public TrainingService(INotificationContext notificationContext, IUnitOfWork unitOfWork,
            IEntityValidationService entityValidationService, IValidator<CreateTrainingSessionDTO> createTrainingSessionValidator, IValidator<CreateTrainingAttendanceDTO> createTrainingAttendanceValidator)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _entityValidationService = entityValidationService;
            _createTrainingSessionValidator = createTrainingSessionValidator;
            _createTrainingAttendanceValidator = createTrainingAttendanceValidator;
        }

        #region TrainingSession
        public async Task<TrainingSession?> CreateTrainingSession(CreateTrainingSessionDTO trainingSessionBody)
        {
            bool validationResult = _entityValidationService.Validate(_createTrainingSessionValidator, trainingSessionBody);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.TrainingSessionNotifications.ERROR_TRAINING_SESSION_CREATED, string.Empty);
                return null;
            }

            TrainingSession trainingSession = new();
            trainingSession.SetTeamId(trainingSessionBody.TeamId);
            trainingSession.SetName(trainingSessionBody.Name);
            trainingSession.SetDate(trainingSessionBody.Date);
            trainingSession.SetDuration(trainingSessionBody.Duration);
            trainingSession.SetLocation(trainingSessionBody.Location);
            trainingSession.SetObjectives(trainingSessionBody.Objectives);
            trainingSession.SetDescription(trainingSessionBody.Description);
            trainingSession.SetIsCanceled(trainingSessionBody.IsCanceled);

            return await _unitOfWork.TrainingSessionRepository.AddAsync(trainingSession);
        }

        public async Task<TrainingSession?> UpdateTrainingSession(UpdateTrainingSessionDTO trainingSessionToUpdate, TrainingSession trainingSession)
        {
            CreateTrainingSessionDTO trainingSessionBody = new() 
            {
                TeamId = trainingSessionToUpdate.TeamId,
                Name = trainingSessionToUpdate.Name,
                Date = trainingSessionToUpdate.Date,
                Duration = trainingSessionToUpdate.Duration,
                IsCanceled = trainingSessionToUpdate.IsCanceled,
                Location = trainingSessionToUpdate.Location,
                Objectives = trainingSessionToUpdate.Objectives,
                Description = trainingSessionToUpdate.Description
            };

            bool validationResult = _entityValidationService.Validate(_createTrainingSessionValidator, trainingSessionBody);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.TrainingSessionNotifications.ERROR_TRAINING_SESSION_UPDATED, string.Empty);
                return null;
            }

            trainingSession.SetTeamId(trainingSessionBody.TeamId);
            trainingSession.SetName(trainingSessionBody.Name);
            trainingSession.SetDate(trainingSessionBody.Date);
            trainingSession.SetDuration(trainingSessionBody.Duration);
            trainingSession.SetLocation(trainingSessionBody.Location);
            trainingSession.SetObjectives(trainingSessionBody.Objectives);
            trainingSession.SetDescription(trainingSessionBody.Description);
            trainingSession.SetIsCanceled(trainingSessionBody.IsCanceled);

            _unitOfWork.TrainingSessionRepository.Update(trainingSession);
            return trainingSession;
        }

        public async Task<TrainingSession?> DeleteTrainingSession(long id)
        {
            TrainingSession? trainingSession = await _unitOfWork.TrainingSessionRepository.GetById(id);

            if (trainingSession == null)
            {
                _notificationContext.AddNotification(NotificationKeys.TrainingSessionNotifications.TRAINING_SESSION_DOES_NOT_EXIST, string.Empty);
                return null;
            }

            _unitOfWork.TrainingSessionRepository.Delete(trainingSession);
            return trainingSession;
        }
        #endregion

        #region TrainingAttendance
        public async Task<TrainingAttendance?> CreateTrainingAttendance(CreateTrainingAttendanceDTO trainingAttendanceBody)
        {
            bool validationResult = _entityValidationService.Validate(_createTrainingAttendanceValidator, trainingAttendanceBody);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.TrainingAttendanceNotifications.ERROR_TRAINING_ATTENDANCE_CREATED, string.Empty);
                return null;
            }

            TrainingAttendance trainingAttendance = new();
            trainingAttendance.SetTrainingSessionId(trainingAttendanceBody.TrainingSessionId);
            trainingAttendance.SetPlayerId(trainingAttendanceBody.PlayerId);
            trainingAttendance.SetIsPresent(trainingAttendanceBody.IsPresent);
            trainingAttendance.SetAbsenceReason(trainingAttendanceBody.AbsenceReason);

            return await _unitOfWork.TrainingAttendanceRepository.AddAsync(trainingAttendance);
        }

        public async Task<TrainingAttendance?> DeleteTrainingAttendance(long id)
        {
            TrainingAttendance? trainingAttendance = await _unitOfWork.TrainingAttendanceRepository.GetById(id);

            if (trainingAttendance == null)
            {
                _notificationContext.AddNotification(NotificationKeys.TrainingAttendanceNotifications.TRAINING_ATTENDANCE_DOES_NOT_EXIST, string.Empty);
                return null;
            }

            _unitOfWork.TrainingAttendanceRepository.Delete(trainingAttendance);
            return trainingAttendance;
        }

        public async Task<TrainingAttendance?> UpdateTrainingAttendance(UpdateTrainingAttendanceDTO trainingAttendanceToUpdate, TrainingAttendance trainingAttendance)
        {
            CreateTrainingAttendanceDTO trainingAttendanceBody = new()
            {
                TrainingSessionId = trainingAttendanceToUpdate.TrainingSessionId,
                PlayerId = trainingAttendanceToUpdate.PlayerId,
                IsPresent = trainingAttendanceToUpdate.IsPresent,
                AbsenceReason = trainingAttendanceToUpdate.AbsenceReason
            };

            bool validationResult = _entityValidationService.Validate(_createTrainingAttendanceValidator, trainingAttendanceBody);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.TrainingAttendanceNotifications.ERROR_TRAINING_ATTENDANCE_UPDATED, string.Empty);
                return null;
            }

            trainingAttendance.SetTrainingSessionId(trainingAttendanceBody.TrainingSessionId);
            trainingAttendance.SetPlayerId(trainingAttendanceBody.PlayerId);
            trainingAttendance.SetIsPresent(trainingAttendanceBody.IsPresent);
            trainingAttendance.SetAbsenceReason(trainingAttendanceBody.AbsenceReason);

            _unitOfWork.TrainingAttendanceRepository.Update(trainingAttendance);
            return trainingAttendance;
        }
        #endregion
    }
}
