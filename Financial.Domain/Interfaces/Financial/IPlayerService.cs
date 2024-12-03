using Contracts;
using Financial.Domain.Entities;

namespace Financial.Domain.Interfaces.Identity
{
    public interface IPlayerService
    {
        Task<Player?> DeletePlayer(Player? player);
        Task<Player?> CreatePlayer(CreateUpdatePlayer playerBody);
        Task<Player?> UpdatePlayer(CreateUpdatePlayer playerToUpdate, Player player);
    }
}