using Infrastructures.Domain.Entities;
using Infrastructures.Domain.Interfaces.Repositories.Identity;

namespace Infrastructures.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();

        //Infrastructures
        IBaseRepository<FacilityCategory> FacilityCategoryRepository { get; }
        IBaseRepository<Facility> FacilityRepository { get; }
        IBaseRepository<FacilityReservation> FacilityReservationRepository { get; }
        IBaseRepository<MaintenanceRequest> MaintenanceRequestRepository { get; }
        IMaintenanceHistoryRepository MaintenanceHistoryRepository { get; }
        IBaseRepository<User> _userRepository { get; }

    }
}