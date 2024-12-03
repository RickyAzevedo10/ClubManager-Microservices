using Identity.Domain.Entities;

namespace Identity.Application.Interfaces.Producers
{
    public interface IInstitutionProducer
    {
        Task CreateUpdateInstitutionProducer(Institution institution);
        Task DeleteInstitutionProducer(long userId);
    }
}