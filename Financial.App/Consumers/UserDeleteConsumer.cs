using Contracts;
using Financial.Domain.Entities;
using Financial.Domain.Interfaces.Financial;
using Financial.Domain.Interfaces.Repositories;
using MassTransit;

namespace Financial.Application.Consumers
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
