using Contracts;
using MembersTeams.Domain.Entities;
using MembersTeams.Domain.Interfaces.Identity;
using MembersTeams.Domain.Interfaces.Repositories;

namespace MembersTeams.Domain.Services.Identity
{
    public class InstitutionService : IInstitutionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InstitutionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Institution?> Create(CreateUpdateInstitution institutionBody)
        {
            Institution institution = new();
            institution.SetName(institutionBody.Name);
            institution.SetExternalInstitutionId(institutionBody.ExternalInstitutionId);

            return await _unitOfWork.InstitutionRepository.AddAsync(institution);
        }

        public async Task<Institution?> Update(CreateUpdateInstitution institutionToUpdate, Institution institution)
        {
            institution.SetName(institutionToUpdate.Name);
            institution.SetExternalInstitutionId(institutionToUpdate.ExternalInstitutionId);

            _unitOfWork.InstitutionRepository.Update(institution);
            return institution;
        }

        public async Task<Institution?> Delete(Institution institution)
        {
            _unitOfWork.InstitutionRepository.Delete(institution);
            return institution;
        }
    }
}
