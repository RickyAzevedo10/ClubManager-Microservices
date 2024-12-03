using AutoMapper;
using TrainingCompetition.Domain.DTOs;
using TrainingCompetition.Domain.Entities;

namespace TrainingCompetition.Application.Mappings
{
    public class TrainingCompetitionMappingProfile : Profile
    {
        public TrainingCompetitionMappingProfile()
        {
            CreateMap<Match, MatchResponseDTO>();
            CreateMap<MatchStatistic, MatchStatisticResponseDTO>();
            CreateMap<TrainingSession, TrainingSessionResponseDTO>();
            CreateMap<TrainingAttendance, TrainingAttendanceResponseDTO>();
        }
    }
}
