using Contracts;
using MassTransit;
using MembersTeams.Domain.Entities;
using MembersTeams.Domain.Interfaces.Identity;
using MembersTeams.Domain.Interfaces.Repositories;

namespace MembersTeams.Application.Consumers
{
    public class InstitutionDeleteConsumer : IConsumer<DeleteInstitution>
    {
        private readonly IInstitutionService _institutionService;
        private readonly IUnitOfWork _unitOfWork;

        public InstitutionDeleteConsumer(IInstitutionService institutionService, IUnitOfWork unitOfWork)
        {
            _institutionService = institutionService;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<DeleteInstitution> context)
        {
            Institution? institution = await _unitOfWork.InstitutionRepository.GetById(context.Message.ExternalInstitutionId);

            if (institution != null)
            {
                //delete
                await _institutionService.Delete(institution);
                await _unitOfWork.CommitAsync();
            }

            return;
        }
    }
}
