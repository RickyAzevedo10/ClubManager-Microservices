using Contracts;
using MembersTeams.Domain.Entities;
using MembersTeams.Domain.Interfaces.Financial;
using MembersTeams.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace MembersTeams.Domain.Services.Identity
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<User?> Get(long id)
        {
            User? user = await _unitOfWork.UserRepository.GetById(id);
            return user;
        }

        public async Task<User?> Create(CreateUpdateUser createUser)
        {
            User user = new()
            {
                ExternalUserId = createUser.ExternalUserId,
                Email = createUser.Email,
                PhoneNumber = createUser.PhoneNumber,
                Username = createUser.Username
            };

            return await _unitOfWork.UserRepository.AddAsync(user);
        }

        public async Task<User?> Update(CreateUpdateUser userToUpdate, User user)
        {
            user.SetEmail(userToUpdate.Email);
            user.SetUsername(userToUpdate.Username);
            user.SetPhoneNumber(userToUpdate.PhoneNumber);

            _unitOfWork.UserRepository.Update(user);
            return user;
        }

        public async Task<User?> Delete(User user)
        {
            _unitOfWork.UserRepository.Delete(user);
            return user;
        }
    }
}
