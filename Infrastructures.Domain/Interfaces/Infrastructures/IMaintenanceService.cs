using Infrastructures.Domain.DTOs;
using Infrastructures.Domain.Entities;

namespace Infrastructures.Domain.Interfaces.Infrastructures
{
    public interface IMaintenanceService
    {
        Task<MaintenanceRequest?> DeleteMaintenanceRequest(long id);
        Task<MaintenanceRequest?> CreateMaintenanceRequest(CreateMaintenanceRequestDTO maintenanceRequestBody);
        Task<MaintenanceRequest?> UpdateMaintenanceRequest(UpdateMaintenanceRequestDTO maintenanceRequestToUpdate, MaintenanceRequest maintenanceRequest);
        Task<MaintenanceHistory?> CreateMaintenanceHistory(long maintenanceRequestId);
    }
}
