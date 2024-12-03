using Infrastructures.Domain.DTOs;
using Infrastructures.Domain.Entities;

namespace Infrastructures.Domain.Interfaces.Infrastructures
{
    public interface IFacilityService
    {
        Task<Facility?> DeleteFacility(long id);
        Task<Facility?> CreateFacility(CreateFacilityDTO facilityBody);
        Task<Facility?> UpdateFacility(UpdateFacilityDTO facilityToUpdate, Facility facility);
        Task<FacilityReservation?> DeleteFacilityReservation(long id);
        Task<FacilityReservation?> CreateFacilityReservation(CreateFacilityReservationDTO facilityReservationBody);
        Task<FacilityReservation?> UpdateFacilityReservation(UpdateFacilityReservationDTO facilityReservationToUpdate, FacilityReservation facilityReservation);
    }
}
