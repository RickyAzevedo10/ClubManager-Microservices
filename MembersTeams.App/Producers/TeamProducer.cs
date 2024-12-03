using Contracts;
using MassTransit;
using MembersTeams.Application.Interfaces.Producers;
using MembersTeams.Domain.Entities;

namespace MembersTeams.Application.Producers
{
    public class TeamProducer : ITeamProducer
    {
        private readonly IPublishEndpoint _publishpoint;

        public TeamProducer(IPublishEndpoint publishEndpoint)
        {
            _publishpoint = publishEndpoint;
        }

        public async Task CreateUpdateTeamProducer(Team team)
        {
            CreateUpdateTeam createTeam = new()
            {
                Name = team.Name,
                Male = team.Male,
                Female = team.Female,
                ExternalTeamId = team.Id
            };

            await _publishpoint.Publish(createTeam);
        }

        public async Task DeleteTeamProducer(long teamId)
        {
            DeleteTeam deleteTeam = new()
            {
                ExternalTeamId = teamId
            };

            await _publishpoint.Publish(deleteTeam);
        }

    }
}
