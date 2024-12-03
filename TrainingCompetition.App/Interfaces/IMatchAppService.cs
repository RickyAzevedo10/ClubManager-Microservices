using TrainingCompetition.Domain.DTOs;

namespace TrainingCompetition.Application.Interfaces
{
    public interface IMatchAppService
    {
        Task<MatchResponseDTO?> CreateMatch(CreateMatchDTO matchBody);
        Task<MatchResponseDTO?> DeleteMatch(long id);
        Task<MatchResponseDTO?> GetMatch(long matchId);
        Task<List<MatchResponseDTO>?> GetTeamMatches(long teamId);
        Task<MatchResponseDTO?> UpdateMatch(UpdateMatchDTO matchToUpdate);
        Task<MatchStatisticResponseDTO?> DeleteMatchStatistic(long id);
        Task<List<MatchStatisticResponseDTO>?> GetMatchStatisticsFromMatchID(long matchId);
        Task<List<MatchStatisticResponseDTO>?> GetPlayerMatchStatistics(long playerId);
        Task<List<MatchStatisticResponseDTO>?> GetPlayerMatchStatisticsFromMatchId(long playerId, long matchId);
        Task<MatchStatisticResponseDTO?> CreateMatchStatistic(CreateMatchStatisticDTO matchStatisticBody);
        Task<MatchStatisticResponseDTO?> UpdateMatchStatistic(UpdateMatchStatisticDTO matchStatisticToUpdate);
    }
}
