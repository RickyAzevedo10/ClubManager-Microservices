using Identity.Domain.DTOs;
using Identity.Domain.Entities;

namespace Identity.Domain.Interfaces.Identity
{
    public interface IInstitutionService
    {
        Task<Institution?> Get(long id);
        Task<List<Institution>?> GetAll();
        Task<Institution?> Create(CreateInstitutionDTO institutionBody);
        Task<Institution?> Update(UpdateInstitutionDTO institutionToUpdate, Institution institution);
        Task<Institution?> Delete(long id);
    }
}