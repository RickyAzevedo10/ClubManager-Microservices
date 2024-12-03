using AutoMapper;
using TrainingCompetition.Application.Interfaces;
using TrainingCompetition.Application.Interfaces.Infrastructure;
using TrainingCompetition.Domain.DTOs;
using TrainingCompetition.Domain.Entities;
using TrainingCompetition.Domain.Interfaces;
using TrainingCompetition.Domain.Interfaces.Identity;
using TrainingCompetition.Domain.Interfaces.Repositories;
using static TrainingCompetition.Domain.Constants.Constants;

namespace TrainingCompetition.Application.Services
{
    public class TrainingAppService : ITrainingAppService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITrainingService _trainingService;
        private readonly IMapper _mapper;
        private readonly IUserPermissionsCachedRepository _userPermissionsCachedRepository;

        public TrainingAppService(INotificationContext notificationContext, IUnitOfWork unitOfWork, IUserPermissionsCachedRepository userPermissionsCachedRepository,
            ITrainingService trainingService, IMapper mapper)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _trainingService = trainingService;
            _mapper = mapper;
            _userPermissionsCachedRepository = userPermissionsCachedRepository;
        }

        #region TrainingSession
        public async Task<TrainingSessionResponseDTO?> CreateTrainingSession(CreateTrainingSessionDTO trainingSessionBody)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Create)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CREATE, string.Empty);
                return null;
            }

            TrainingSession? trainingSession = await _trainingService.CreateTrainingSession(trainingSessionBody);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<TrainingSessionResponseDTO>(trainingSession);
        }

        public async Task<TrainingSessionResponseDTO?> DeleteTrainingSession(long id)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Delete)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_DELETE, string.Empty);
                return null;
            }

            TrainingSession? trainingSessionDeleted = await _trainingService.DeleteTrainingSession(id);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<TrainingSessionResponseDTO>(trainingSessionDeleted);
        }

        public async Task<TrainingSessionResponseDTO?> GetTrainingSession(long trainingSessionId)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            TrainingSession trainingSession = await _unitOfWork.TrainingSessionRepository.GetById(trainingSessionId);

            return _mapper.Map<TrainingSessionResponseDTO>(trainingSession);
        }

        public async Task<TrainingSessionResponseDTO?> UpdateTrainingSession(UpdateTrainingSessionDTO trainingSessionToUpdate)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Edit)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_EDIT, string.Empty);
                return null;
            }

            TrainingSession? trainingSession = null;

            if (trainingSessionToUpdate.Id != null)
                trainingSession = await _unitOfWork.TrainingSessionRepository.GetById(trainingSessionToUpdate.Id);

            if (trainingSession == null)
            {
                _notificationContext.AddNotification(NotificationKeys.TrainingSessionNotifications.TRAINING_SESSION_DOES_NOT_EXIST, string.Empty);
                return null;
            }

            trainingSession = await _trainingService.UpdateTrainingSession(trainingSessionToUpdate, trainingSession);

            if (trainingSession == null)
            {
                _notificationContext.AddNotification(NotificationKeys.TrainingSessionNotifications.ERROR_TRAINING_SESSION_UPDATED, string.Empty);
                return null;
            }

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<TrainingSessionResponseDTO>(trainingSession);
        }

        public async Task<List<TrainingSessionResponseDTO>?> GetTrainingSessionsByDateRange(DateTime startDate, DateTime endDate)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            List<TrainingSession> trainingSession = await _unitOfWork.TrainingSessionRepository.GetTrainingSessionsByDateRangeAsync(startDate, endDate);

            return _mapper.Map<List<TrainingSessionResponseDTO>>(trainingSession);
        }
        #endregion

        #region TrainingAttendance
        public async Task<TrainingAttendanceResponseDTO?> CreateTrainingAttendance(CreateTrainingAttendanceDTO trainingAttendanceBody)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Create)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CREATE, string.Empty);
                return null;
            }

            TrainingAttendance? trainingAttendance = await _trainingService.CreateTrainingAttendance(trainingAttendanceBody);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<TrainingAttendanceResponseDTO>(trainingAttendance);
        }

        public async Task<TrainingAttendanceResponseDTO?> DeleteTrainingAttendance(long id)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Delete)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_DELETE, string.Empty);
                return null;
            }

            TrainingAttendance? trainingAttendanceDeleted = await _trainingService.DeleteTrainingAttendance(id);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<TrainingAttendanceResponseDTO>(trainingAttendanceDeleted);
        }

        public async Task<TrainingAttendanceResponseDTO?> UpdateTrainingAttendance(UpdateTrainingAttendanceDTO trainingAttendanceToUpdate)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Edit)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_EDIT, string.Empty);
                return null;
            }

            TrainingAttendance? trainingAttendance = null;

            if (trainingAttendanceToUpdate.Id != null)
                trainingAttendance = await _unitOfWork.TrainingAttendanceRepository.GetById(trainingAttendanceToUpdate.Id);

            if (trainingAttendance == null)
            {
                _notificationContext.AddNotification(NotificationKeys.TrainingAttendanceNotifications.TRAINING_ATTENDANCE_DOES_NOT_EXIST, string.Empty);
                return null;
            }

            trainingAttendance = await _trainingService.UpdateTrainingAttendance(trainingAttendanceToUpdate, trainingAttendance);

            if (trainingAttendance == null)
            {
                _notificationContext.AddNotification(NotificationKeys.TrainingAttendanceNotifications.ERROR_TRAINING_ATTENDANCE_UPDATED, string.Empty);
                return null;
            }

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<TrainingAttendanceResponseDTO>(trainingAttendance);
        }

        public async Task<List<TrainingAttendanceResponseDTO>?> GetTrainingAttendance(long trainingAttendanceId)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            List<TrainingAttendance>? trainingAttendance = await _unitOfWork.TrainingAttendanceRepository.GetTrainingAttendanceByPlayerId(trainingAttendanceId);

            return _mapper.Map<List<TrainingAttendanceResponseDTO>>(trainingAttendance);
        }
        #endregion
    }
}
