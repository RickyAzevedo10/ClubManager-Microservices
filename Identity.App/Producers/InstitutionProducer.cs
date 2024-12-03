using Contracts;
using Identity.Application.Interfaces.Producers;
using Identity.Domain.Entities;
using MassTransit;

namespace Identity.Application.Producers
{
    public class InstitutionProducer : IInstitutionProducer
    {
        private readonly IPublishEndpoint _publishpoint;

        public InstitutionProducer(IPublishEndpoint publishEndpoint)
        {
            _publishpoint = publishEndpoint;
        }

        public async Task CreateUpdateInstitutionProducer(Institution institution)
        {
            CreateUpdateInstitution createInstitution = new()
            {
                Name = institution.Name,
                ExternalInstitutionId = institution.Id
            };

            await _publishpoint.Publish(createInstitution);
        }

        public async Task DeleteInstitutionProducer(long instutionId)
        {
            DeleteInstitution deleteInstitution = new()
            {
                ExternalInstitutionId = instutionId
            };

            await _publishpoint.Publish(deleteInstitution);
        }

    }
}
