using MembersTeams.Domain.Entities;

namespace MembersTeams.Application.Interfaces.Producers
{
    public interface IPlayerProducer
    {
        Task CreateUpdatePlayerProducer(Player player);
        Task DeletePlayerProducer(long playerId);
    }
}