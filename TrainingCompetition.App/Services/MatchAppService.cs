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
    public class MatchAppService : IMatchAppService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMatchService _matchService;
        private readonly IMapper _mapper;
        private readonly IUserPermissionsCachedRepository _userPermissionsCachedRepository;

        public MatchAppService(INotificationContext notificationContext, IUnitOfWork unitOfWork, IMatchService matchAppService, IMapper mapper, IUserPermissionsCachedRepository userPermissionsCachedRepository)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _matchService = matchAppService;
            _mapper = mapper;
            _userPermissionsCachedRepository = userPermissionsCachedRepository;
        }

        #region Match
        public async Task<MatchResponseDTO?> CreateMatch(CreateMatchDTO matchBody)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Create)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CREATE, string.Empty);
                return null;
            }

            Match? match = await _matchService.CreateMatch(matchBody);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<MatchResponseDTO>(match);
        }

        public async Task<MatchResponseDTO?> DeleteMatch(long id)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Delete)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_DELETE, string.Empty);
                return null;
            }

            Match? matchDeleted = await _matchService.DeleteMatch(id);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<MatchResponseDTO>(matchDeleted);
        }

        public async Task<MatchResponseDTO?> GetMatch(long matchId)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            Match match = await _unitOfWork.MatchRepository.GetById(matchId);

            return _mapper.Map<MatchResponseDTO>(match);
        }

        public async Task<List<MatchResponseDTO>?> GetTeamMatches(long teamId)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            List<Match>? match = await _unitOfWork.MatchRepository.GetMachesByTeamId(teamId);

            return _mapper.Map<List<MatchResponseDTO>>(match);
        }

        public async Task<MatchResponseDTO?> UpdateMatch(UpdateMatchDTO matchToUpdate)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Edit)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_EDIT, string.Empty);
                return null;
            }

            Match? match = null;

            if (matchToUpdate.Id != null)
                match = await _unitOfWork.MatchRepository.GetById((long)matchToUpdate.Id);

            if (match == null)
            {
                _notificationContext.AddNotification(NotificationKeys.MatchNotifications.MATCH_DOES_NOT_EXIST, string.Empty);
                return null;
            }

            match = await _matchService.UpdateMatch(matchToUpdate, match);

            if (match == null)
            {
                _notificationContext.AddNotification(NotificationKeys.MatchNotifications.ERROR_MATCH_UPDATED, string.Empty);
                return null;
            }

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<MatchResponseDTO>(match);
        }
        #endregion

        #region MatchStatistics
        public async Task<MatchStatisticResponseDTO?> CreateMatchStatistic(CreateMatchStatisticDTO matchStatisticBody)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Create)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CREATE, string.Empty);
                return null;
            }

            MatchStatistic? matchStatistic = await _matchService.CreateMatchStatistic(matchStatisticBody);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<MatchStatisticResponseDTO>(matchStatistic);
        }

        public async Task<MatchStatisticResponseDTO?> UpdateMatchStatistic(UpdateMatchStatisticDTO matchStatisticToUpdate)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Edit)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_EDIT, string.Empty);
                return null;
            }

            MatchStatistic? matchStatistic = null;

            if (matchStatisticToUpdate.Id != null)
                matchStatistic = await _unitOfWork.MatchStatisticRepository.GetById(matchStatisticToUpdate.Id);

            if (matchStatistic == null)
            {
                _notificationContext.AddNotification(NotificationKeys.MatchStatisticNotifications.MATCH_STATISTIC_DOES_NOT_EXIST, string.Empty);
                return null;
            }

            matchStatistic = await _matchService.UpdateMatchStatistic(matchStatisticToUpdate, matchStatistic);

            if (matchStatistic == null)
            {
                _notificationContext.AddNotification(NotificationKeys.MatchStatisticNotifications.ERROR_MATCH_STATISTIC_UPDATED, string.Empty);
                return null;
            }

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<MatchStatisticResponseDTO>(matchStatistic);
        }

        public async Task<MatchStatisticResponseDTO?> DeleteMatchStatistic(long id)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Delete)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_DELETE, string.Empty);
                return null;
            }

            MatchStatistic? matchStatisticDeleted = await _matchService.DeleteMatchStatistic(id);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<MatchStatisticResponseDTO>(matchStatisticDeleted);
        }

        public async Task<List<MatchStatisticResponseDTO>?> GetMatchStatisticsFromMatchID(long matchId)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            List<MatchStatistic>? matchStatisticsList = await _unitOfWork.MatchStatisticRepository.GetMatchStatisticsFromMatchID(matchId);

            return _mapper.Map<List<MatchStatisticResponseDTO>>(matchStatisticsList);
        }

        public async Task<List<MatchStatisticResponseDTO>?> GetPlayerMatchStatistics(long playerId)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            List<MatchStatistic>? matchStatisticsList = await _unitOfWork.MatchStatisticRepository.GetPlayerMatchStatistics(playerId);

            return _mapper.Map<List<MatchStatisticResponseDTO>>(matchStatisticsList);
        }

        public async Task<List<MatchStatisticResponseDTO>?> GetPlayerMatchStatisticsFromMatchId(long playerId, long matchId)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            List<MatchStatistic>? matchStatisticsList = await _unitOfWork.MatchStatisticRepository.GetPlayerMatchStatisticsFromMatchId(playerId, matchId);

            return _mapper.Map<List<MatchStatisticResponseDTO>>(matchStatisticsList);
        }
        #endregion
    }
}
