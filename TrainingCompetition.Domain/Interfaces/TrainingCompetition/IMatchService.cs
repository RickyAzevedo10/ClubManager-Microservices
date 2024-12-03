using TrainingCompetition.Domain.DTOs;
using TrainingCompetition.Domain.Entities;

namespace TrainingCompetition.Domain.Interfaces.Identity
{
    public interface IMatchService
    {
        Task<Match?> CreateMatch(CreateMatchDTO matchBody);
        Task<Match?> DeleteMatch(long id);
        Task<Match?> UpdateMatch(UpdateMatchDTO matchToUpdate, Match match);
        Task<MatchStatistic?> DeleteMatchStatistic(long id);
        Task<MatchStatistic?> CreateMatchStatistic(CreateMatchStatisticDTO matchStatisticBody);
        Task<MatchStatistic?> UpdateMatchStatistic(UpdateMatchStatisticDTO MatchStatisticToUpdate, MatchStatistic matchStatistic);
    }
}