using AutoMapper;
using Infrastructures.Domain.DTOs;
using Infrastructures.Domain.Entities;

namespace Infrastructures.Application.Mappings
{
    public class InfrastructuresMappingProfile : Profile
    {
        public InfrastructuresMappingProfile()
        {
            CreateMap<Facility, FacilityResponseDTO>();
            CreateMap<FacilityCategory, FacilityCategoryResponseDTO>();
            CreateMap<FacilityReservation, FacilityReservationResponseDTO>();
            CreateMap<MaintenanceRequest, MaintenanceRequestResponseDTO>();
        }
    }
}
