using Contracts;
using MembersTeams.Domain.Entities;

namespace MembersTeams.Domain.Interfaces.Identity
{
    public interface IInstitutionService
    {
        Task<Institution?> Create(CreateUpdateInstitution institutionBody);
        Task<Institution?> Update(CreateUpdateInstitution institutionToUpdate, Institution institution);
        Task<Institution?> Delete(Institution institution);
    }
}