using Contracts;
using Infrastructures.Domain.Entities;
using Infrastructures.Domain.Interfaces.Financial;
using Infrastructures.Domain.Interfaces.Repositories;
using MassTransit;

namespace Infrastructures.Application.Consumers
{
    public class UserDeleteConsumer : IConsumer<DeleteUser>
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public UserDeleteConsumer(IUserService userService, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<DeleteUser> context)
        {
            User? user = await _userService.Get(context.Message.ExternalUserId);

            if (user != null)
            {
                //delete
                await _userService.Delete(user);
                await _unitOfWork.CommitAsync();
            }

            return;
        }
    }
}
