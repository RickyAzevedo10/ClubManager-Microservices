using Identity.Domain.Entities;

namespace Identity.Domain.Interfaces.Repositories.Identity
{
    public interface IInstitutionRepository : IBaseRepository<Institution>
    {
        Task<Institution?> GetByIdAsync(long userId);
        Task<Institution?> GetByNameAsync(string name);
    }
}