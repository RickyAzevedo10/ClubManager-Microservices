using Financial.Domain.DTOs;
using Financial.Domain.Entities;

namespace Financial.Domain.Interfaces.Identity
{
    public interface IEntityService
    {
        Task<Entity?> CreateEntity(CreateEntityDTO entityBody);
        Task<Entity?> UpdateEntity(UpdateEntityDTO entityToUpdate, Entity entity);
        Task<Entity?> DeleteEntity(long id);
    }
}