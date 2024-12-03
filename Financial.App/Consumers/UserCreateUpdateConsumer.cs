﻿using Contracts;
using Financial.Domain.Entities;
using Financial.Domain.Interfaces.Financial;
using Financial.Domain.Interfaces.Repositories;
using MassTransit;

namespace Financial.Application.Consumers
{
    public class UserCreateUpdateConsumer : IConsumer<CreateUpdateUser>
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public UserCreateUpdateConsumer(IUserService userService, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<CreateUpdateUser> context)
        {
            User? user = await _userService.Get(context.Message.ExternalUserId);

            if (user == null)
            {
                //create
                await _userService.Create(context.Message);
            }
            else
            {
                //update
                await _userService.Update(context.Message, user);
            }
            await _unitOfWork.CommitAsync();
            return;
        }
    }
}
