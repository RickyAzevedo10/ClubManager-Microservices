using Financial.Domain.DTOs;
using Financial.Domain.Entities;

namespace Financial.Domain.Interfaces.Identity
{
    public interface IRevenueService
    {
        Task<Revenue?> CreateRevenue(RevenueDTO revenueBody);
        Task<Revenue?> DeleteRevenue(long id);
        Task<Revenue?> UpdateRevenue(UpdateRevenueDTO revenueBody);
    }
}