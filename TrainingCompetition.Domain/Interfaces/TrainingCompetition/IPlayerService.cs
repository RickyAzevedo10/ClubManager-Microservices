using Contracts;
using TrainingCompetition.Domain.Entities;

namespace TrainingCompetition.Domain.Interfaces.Identity
{
    public interface IPlayerService
    {
        Task<Player?> DeletePlayer(Player? player);
        Task<Player?> CreatePlayer(CreateUpdatePlayer playerBody);
        Task<Player?> UpdatePlayer(CreateUpdatePlayer playerToUpdate, Player player);
    }
}