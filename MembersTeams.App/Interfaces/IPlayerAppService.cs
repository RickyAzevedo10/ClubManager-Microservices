using MembersTeams.Domain.DTOs;

namespace MembersTeams.Application.Interfaces
{
    public interface IPlayerAppService
    {
        Task<PlayerResponseDTO?> GetPlayer(long id);
        Task<PlayerResponseDTO?> CreatePlayer(CreatePlayerDTO playerBody);
        Task<PlayerResponseDTO?> UpdatePlayer(UpdatePlayerDTO playerToUpdate);
        Task<List<PlayerResponseDTO>?> SearchPlayersAsync(string? firstName, string? lastName, string? position);
        Task<PlayerResponseDTO?> DeletePlayer(long id);

        Task<List<PlayerTransferResponseDTO>?> GetPlayerTransfer(long id);
        Task<PlayerTransferResponseDTO?> DeletePlayerTransfer(long id);
        Task<PlayerTransferResponseDTO?> CreatePlayerTransfer(CreatePlayerTransferDTO playerTransferBody);
        Task<PlayerTransferResponseDTO?> UpdatePlayerTransfer(UpdatePlayerTransferDTO playerTransferToUpdate);

        Task<List<PlayerContractResponseDTO>?> GetPlayerContract(long id);
        Task<PlayerContractResponseDTO?> DeletePlayerContract(long id);
        Task<PlayerContractResponseDTO?> CreatePlayerContract(CreatePlayerContractDTO playerContractBody);
        Task<PlayerContractResponseDTO?> UpdatePlayerContract(UpdatePlayerContractDTO playerContractToUpdate);

        Task<PlayerPerformanceHistorySimpleResponseDTO?> DeletePlayerPerformanceHistory(long id);
        Task<PlayerPerformanceHistorySimpleResponseDTO?> CreatePlayerPerformanceHistory(CreatePlayerPerformanceHistoryDTO playerPerformanceHistoryBody);
        Task<PlayerPerformanceHistorySimpleResponseDTO?> UpdatePlayerPerformanceHistory(UpdatePlayerPerformanceHistoryDTO playerPerformanceHistoryToUpdate);
        Task<PlayerPerformanceHistoryResponseDTO?> GetPlayerPerformanceHistory(long playerId, string season);

    }
}
