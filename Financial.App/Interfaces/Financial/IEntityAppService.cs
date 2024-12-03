using Financial.Domain.DTOs;

namespace Financial.App.Services.Infrastructures
{
    public interface IEntityAppService
    {
        Task<EntityResponseDTO?> DeleteEntity(long id);
        Task<EntityResponseDTO?> CreateEntity(CreateEntityDTO entityBody);
        Task<EntityResponseDTO?> UpdateEntity(UpdateEntityDTO entityToUpdate);
        Task<EntityResponseDTO?> GetEntity(long entityId);
        Task<List<EntityResponseDTO>?> GetAllEntity();
    }
}
