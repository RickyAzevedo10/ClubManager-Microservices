using Contracts;
using MassTransit;
using MembersTeams.Domain.Entities;
using MembersTeams.Domain.Interfaces.Identity;
using MembersTeams.Domain.Interfaces.Repositories;

namespace MembersTeams.Application.Consumers
{
    public class InstitutionCreateUpdateConsumer : IConsumer<CreateUpdateInstitution>
    {
        private readonly IInstitutionService _institutionService;
        private readonly IUnitOfWork _unitOfWork;

        public InstitutionCreateUpdateConsumer(IInstitutionService institutionService, IUnitOfWork unitOfWork)
        {
            _institutionService = institutionService;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<CreateUpdateInstitution> context)
        {
            Institution? institution = await _unitOfWork.InstitutionRepository.GetById(context.Message.ExternalInstitutionId);

            if (institution == null)
            {
                //create
                await _institutionService.Create(context.Message);
            }
            else
            {
                //update
                await _institutionService.Update(context.Message, institution);
            }
            await _unitOfWork.CommitAsync();
            return;
        }
    }
}
