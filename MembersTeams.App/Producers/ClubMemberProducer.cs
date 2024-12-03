using Contracts;
using MassTransit;
using MembersTeams.Application.Interfaces.Producers;
using MembersTeams.Domain.Entities;

namespace MembersTeams.Application.Producers
{
    public class ClubMemberProducer : IClubMemberProducer
    {
        private readonly IPublishEndpoint _publishpoint;

        public ClubMemberProducer(IPublishEndpoint publishEndpoint)
        {
            _publishpoint = publishEndpoint;
        }

        public async Task CreateUpdateTeamProducer(ClubMember clubMember)
        {
            CreateUpdateClubMember createClubMember = new()
            {
                FirstName = clubMember.FirstName,
                LastName = clubMember.LastName,
                PhoneNumber = clubMember.PhoneNumber,
                Email = clubMember.Email,
                Partner = clubMember.Partner,
                ExternalClubMemberId = clubMember.Id
            };

            await _publishpoint.Publish(createClubMember);
        }

        public async Task DeleteClubMemberProducer(long clubMemberId)
        {
            DeleteClubMember deleteClubMember = new()
            {
                ExternalClubMemberId = clubMemberId
            };

            await _publishpoint.Publish(deleteClubMember);
        }

    }
}
