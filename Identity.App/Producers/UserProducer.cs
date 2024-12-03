using Contracts;
using Identity.Application.Interfaces.Producers;
using Identity.Domain.Entities;
using MassTransit;

namespace Identity.Application.Producers
{
    public class UserProducer : IUserProducer
    {
        private readonly IPublishEndpoint _publishpoint;

        public UserProducer(IPublishEndpoint publishEndpoint)
        {
            _publishpoint = publishEndpoint;
        }

        public async Task CreateUpdateUserProducer(User user)
        {
            CreateUpdateUser createUser = new()
            {
                Username = user.Username,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ExternalUserId = user.Id
            };

            await _publishpoint.Publish(createUser);
        }

        public async Task DeleteUserProducer(long userId)
        {
            DeleteUser deleteUser = new()
            {
                ExternalUserId = userId
            };

            await _publishpoint.Publish(deleteUser);
        }

    }
}
