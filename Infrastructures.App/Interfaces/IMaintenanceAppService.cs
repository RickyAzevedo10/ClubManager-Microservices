using Infrastructures.Domain.DTOs;
using Infrastructures.Domain.Entities;

namespace Infrastructures.Application.Interfaces
{
    public interface IMaintenanceAppService
    {
        Task<MaintenanceRequestResponseDTO?> GetMaintenanceRequest(long maintenanceRequestId);
        Task<MaintenanceRequestResponseDTO?> DeleteMaintenanceRequest(long id);
        Task<MaintenanceRequestResponseDTO?> CreateMaintenanceRequest(CreateMaintenanceRequestDTO maintenanceRequestBody);
        Task<MaintenanceRequestResponseDTO?> UpdateMaintenanceRequest(UpdateMaintenanceRequestDTO maintenanceRequestToUpdate);
        Task<MaintenanceHistory?> CreateMaintenanceHistory(long maintenanceRequestId);
        Task<List<MaintenanceHistory>?> GetMaintenanceHistory(DateTime startDate, DateTime endDate);
    }
}
