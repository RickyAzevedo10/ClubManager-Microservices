using MembersTeams.Domain.DTOs;
using MembersTeams.Domain.Entities;

namespace MembersTeams.Domain.Interfaces.Identity
{
    public interface IPlayerService
    {
        Task<PlayerTransfer?> DeletePlayerTransfer(long id);
        Task<Player?> DeletePlayer(long id);
        Task<PlayerContract?> DeletePlayerContract(long id);
        Task<PlayerPerformanceHistory?> DeletePlayerPerformanceHistory(long id);
        Task<PlayerTransfer?> CreatePlayerTransfer(CreatePlayerTransferDTO playerTransferBody);
        Task<PlayerTransfer?> UpdatePlayerTransfer(UpdatePlayerTransferDTO playerTransferToUpdate, PlayerTransfer playerTransfer);
        Task<PlayerContract?> CreatePlayerContract(CreatePlayerContractDTO playerContractBody);
        Task<PlayerContract?> UpdatePlayerContract(UpdatePlayerContractDTO playerContractToUpdate, PlayerContract playerContract);
        Task<PlayerPerformanceHistory?> CreatePlayerPerformanceHistory(CreatePlayerPerformanceHistoryDTO playerPerformanceHistoryBody);
        Task<PlayerPerformanceHistory?> UpdatePlayerPerformanceHistory(UpdatePlayerPerformanceHistoryDTO playerPerformanceHistoryToUpdate, PlayerPerformanceHistory playerPerformanceHistory);
        Task<Player?> CreatePlayer(CreatePlayerDTO playerBody);
        Task<List<PlayerResponsible>?> CreatePlayerResponsible(CreatePlayerDTO playerBody, Player player);
        Task<Player?> UpdatePlayer(UpdatePlayerDTO playerToUpdate, Player player);
        Task<List<PlayerResponsible>?> UpdatePlayerResponsible(UpdatePlayerDTO playerBody, Player player);
    }
}