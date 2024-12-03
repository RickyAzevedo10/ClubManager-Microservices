using Contracts;
using MassTransit;
using MembersTeams.Application.Interfaces.Producers;
using MembersTeams.Domain.Entities;

namespace MembersTeams.Application.Producers
{
    public class PlayerProducer : IPlayerProducer
    {
        private readonly IPublishEndpoint _publishpoint;

        public PlayerProducer(IPublishEndpoint publishEndpoint)
        {
            _publishpoint = publishEndpoint;
        }

        public async Task CreateUpdatePlayerProducer(Player player)
        {
            CreateUpdatePlayer createPlayer = new()
            {
                FirstName = player.FirstName,
                LastName = player.LastName,
                ExternalPlayerId = player.Id
            };

            await _publishpoint.Publish(createPlayer);
        }

        public async Task DeletePlayerProducer(long playerId)
        {
            DeletePlayer deletePlayer = new()
            {
                ExternalPlayerId = playerId
            };

            await _publishpoint.Publish(deletePlayer);
        }

    }
}
