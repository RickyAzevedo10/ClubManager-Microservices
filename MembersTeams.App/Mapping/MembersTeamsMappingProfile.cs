using AutoMapper;
using MembersTeams.Domain.DTOs;
using MembersTeams.Domain.Entities;

namespace MembersTeams.Application.Mappings
{
    public class MembersTeamsMappingProfile : Profile
    {
        public MembersTeamsMappingProfile()
        {
            CreateMap<ClubMember, ClubMemberResponseDTO>();
            CreateMap<MinorClubMember, MinorClubMemberResponseDTO>();
            CreateMap<Team, TeamResponseDTO>();
            CreateMap<TeamCoach, TeamCoachResponseDTO>();
            CreateMap<TeamPlayer, TeamPlayerResponseDTO>();
            CreateMap<Player, PlayerResponseDTO>();
            CreateMap<PlayerResponsible, ResponsiblePlayerResponseDTO>();
            CreateMap<PlayerTransfer, PlayerTransferResponseDTO>();
            CreateMap<PlayerContract, PlayerContractResponseDTO>();
            CreateMap<PlayerPerformanceHistory, PlayerPerformanceHistorySimpleResponseDTO>();
        }
    }
}
