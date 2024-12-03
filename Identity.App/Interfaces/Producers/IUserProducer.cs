using Contracts;
using Identity.Domain.Entities;

namespace Identity.Application.Interfaces.Producers
{
    public interface IUserProducer
    {
        Task CreateUpdateUserProducer(User user);
        Task DeleteUserProducer(long userId);
    }
}