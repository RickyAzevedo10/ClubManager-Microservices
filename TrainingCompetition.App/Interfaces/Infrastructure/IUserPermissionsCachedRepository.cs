using TrainingCompetition.Domain.DTOs;

namespace TrainingCompetition.Application.Interfaces.Infrastructure
{
    public interface IUserPermissionsCachedRepository
    {
        Task<UserPermissionsCacheInformationDTO?> GetUserPermissionsByUserIdAsync(CancellationToken cancellationToken = default);
    }
}
