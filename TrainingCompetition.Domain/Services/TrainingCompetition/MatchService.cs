using TrainingCompetition.Domain.Interfaces;
using TrainingCompetition.Domain.Interfaces.Identity;
using TrainingCompetition.Domain.Interfaces.Repositories;
using FluentValidation;
using TrainingCompetition.Domain.DTOs;
using TrainingCompetition.Domain.Entities;
using static TrainingCompetition.Domain.Constants.Constants;

namespace TrainingCompetition.Domain.Services.Identity
{
    public class MatchService : IMatchService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityValidationService _entityValidationService;
        private readonly IValidator<CreateMatchDTO> _createMatchValidator;
        private readonly IValidator<CreateMatchStatisticDTO> _createMatchStatisticValidator;


        public MatchService(INotificationContext notificationContext, IUnitOfWork unitOfWork,
            IEntityValidationService entityValidationService, IValidator<CreateMatchDTO> createMatchValidator, IValidator<CreateMatchStatisticDTO> createMatchStatisticValidator)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _entityValidationService = entityValidationService;
            _createMatchValidator = createMatchValidator;
            _createMatchStatisticValidator = createMatchStatisticValidator;
        }

        #region Match
        public async Task<Match?> CreateMatch(CreateMatchDTO matchBody)
        {
            bool validationResult = _entityValidationService.Validate(_createMatchValidator, matchBody);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.MatchNotifications.ERROR_MATCH_CREATED, string.Empty);
                return null;
            }

            Match match = new();
            match.SetLocation(matchBody.Location);
            match.SetOpponent(matchBody.Opponent);
            match.SetDate(matchBody.Date);
            match.SetCompetitionType(matchBody.CompetitionType);
            match.SetTeamId(matchBody.TeamId);
            match.SetIsCanceled(matchBody.IsCanceled);

            return await _unitOfWork.MatchRepository.AddAsync(match);
        }

        public async Task<Match?> UpdateMatch(UpdateMatchDTO matchToUpdate, Match match)
        {
            CreateMatchDTO matchBody = new()
            {
                Date = matchToUpdate.Date,
                Opponent = matchToUpdate.Opponent,
                Location = matchToUpdate.Location,
                CompetitionType = matchToUpdate.CompetitionType,
                TeamId = matchToUpdate.TeamId,
                IsCanceled = matchToUpdate.IsCanceled
            };
            bool validationResult = _entityValidationService.Validate(_createMatchValidator, matchBody);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.MatchNotifications.ERROR_MATCH_UPDATED, string.Empty);
                return null;
            }

            match.SetLocation(matchBody.Location);
            match.SetOpponent(matchBody.Opponent);
            match.SetDate(matchBody.Date);
            match.SetCompetitionType(matchBody.CompetitionType);
            match.SetTeamId(matchBody.TeamId);
            match.SetIsCanceled(matchBody.IsCanceled);

            await _unitOfWork.MatchRepository.UpdateAsync(match);
            return match;
        }

        public async Task<Match?> DeleteMatch(long id)
        {
            Match? match = await _unitOfWork.MatchRepository.GetById(id);

            if (match == null)
            {
                _notificationContext.AddNotification(NotificationKeys.MatchNotifications.MATCH_DOES_NOT_EXIST, string.Empty);
                return null;
            }

            _unitOfWork.MatchRepository.Delete(match);
            return match;
        }

        #endregion

        #region MatchStatistics
        public async Task<MatchStatistic?> CreateMatchStatistic(CreateMatchStatisticDTO matchStatisticBody)
        {
            bool validationResult = _entityValidationService.Validate(_createMatchStatisticValidator, matchStatisticBody);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.MatchStatisticNotifications.ERROR_MATCH_STATISTIC_CREATED, string.Empty);
                return null;
            }

            MatchStatistic matchStatistic = new();
            matchStatistic.SetMatchId(matchStatisticBody.MatchId);
            matchStatistic.SetPlayerId(matchStatisticBody.PlayerId);
            matchStatistic.SetGoals(matchStatisticBody.Goals);
            matchStatistic.SetAssists(matchStatisticBody.Assists);
            matchStatistic.SetYellowCards(matchStatisticBody.YellowCards);
            matchStatistic.SetRedCards(matchStatisticBody.RedCards);
            matchStatistic.SetMinutesPlayed(matchStatisticBody.MinutesPlayed);
            matchStatistic.SetSubstitutions(matchStatisticBody.Substitutions);

            return await _unitOfWork.MatchStatisticRepository.AddAsync(matchStatistic);
        }

        public async Task<MatchStatistic?> DeleteMatchStatistic(long id)
        {
            MatchStatistic? matchStatistic = await _unitOfWork.MatchStatisticRepository.GetById(id);

            if (matchStatistic == null)
            {
                _notificationContext.AddNotification(NotificationKeys.MatchStatisticNotifications.MATCH_STATISTIC_DOES_NOT_EXIST, string.Empty);
                return null;
            }

            _unitOfWork.MatchStatisticRepository.Delete(matchStatistic);
            return matchStatistic;
        }

        public async Task<MatchStatistic?> UpdateMatchStatistic(UpdateMatchStatisticDTO MatchStatisticToUpdate, MatchStatistic matchStatistic)
        {
            CreateMatchStatisticDTO matchStatisticBody = new()
            {
                MatchId = MatchStatisticToUpdate.MatchId,
                PlayerId = MatchStatisticToUpdate.PlayerId,
                Goals = MatchStatisticToUpdate.Goals,
                Assists = MatchStatisticToUpdate.Assists,
                YellowCards = MatchStatisticToUpdate.YellowCards,
                RedCards = MatchStatisticToUpdate.RedCards,
                MinutesPlayed = MatchStatisticToUpdate.MinutesPlayed,
                Substitutions = MatchStatisticToUpdate.Substitutions
            };
            bool validationResult = _entityValidationService.Validate(_createMatchStatisticValidator, matchStatisticBody);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.MatchStatisticNotifications.ERROR_MATCH_STATISTIC_UPDATED, string.Empty);
                return null;
            }

            matchStatistic.SetMatchId(matchStatisticBody.MatchId);
            matchStatistic.SetPlayerId(matchStatisticBody.PlayerId);
            matchStatistic.SetGoals(matchStatisticBody.Goals);
            matchStatistic.SetAssists(matchStatisticBody.Assists);
            matchStatistic.SetYellowCards(matchStatisticBody.YellowCards);
            matchStatistic.SetRedCards(matchStatisticBody.RedCards);
            matchStatistic.SetMinutesPlayed(matchStatisticBody.MinutesPlayed);
            matchStatistic.SetSubstitutions(matchStatisticBody.Substitutions);

            await _unitOfWork.MatchStatisticRepository.UpdateAsync(matchStatistic);
            return matchStatistic;
        }
        #endregion
    }
}
