using Contracts;
using Identity.App.Interfaces.Infrastructure;
using Identity.Domain.Entities;
using Identity.Domain.Interfaces.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Infra.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserClaimsService _userClaimsService;

        public AuthorizationService(IUnitOfWork unitOfWork, IUserClaimsService userClaimsService)
        {
            _unitOfWork = unitOfWork;
            _userClaimsService = userClaimsService;
        }

        public async Task<bool> CanEdit(string? userEmail = "")
        {
            User? userAuthenticated = await GetUserAuthenticated(userEmail);

            bool canEdit = false;

            if (userAuthenticated != null)
                canEdit = _unitOfWork.UserPermissionsRepository.GetEntity().Where(x => x.Users.Id == userAuthenticated.Id).FirstOrDefault()?.Edit ?? false;

            return canEdit;
        }

        public async Task<bool> CanConsult(string? userEmail = "")
        {
            User? userAuthenticated = await GetUserAuthenticated(userEmail);

            bool canConsult = false;

            if (userAuthenticated != null)
                canConsult = _unitOfWork.UserPermissionsRepository.GetEntity().Where(x => x.Users.Id == userAuthenticated.Id).FirstOrDefault()?.Consult ?? false;

            return canConsult;
        }

        public async Task<bool> CanDelete(string? userEmail = "")
        {
            User? userAuthenticated = await GetUserAuthenticated(userEmail);

            bool canDelete = false;

            if (userAuthenticated != null)
                canDelete = _unitOfWork.UserPermissionsRepository.GetEntity().Where(x => x.Users.Id == userAuthenticated.Id).FirstOrDefault()?.Delete ?? false;

            return canDelete;
        }

        public async Task<bool> CanCreate(string? userEmail = "")
        {
            User? userAuthenticated = await GetUserAuthenticated(userEmail);

            bool canCreate = false;

            if (userAuthenticated != null)
                canCreate = _unitOfWork.UserPermissionsRepository.GetEntity().Where(x => x.Users.Id == userAuthenticated.Id).FirstOrDefault()?.Create ?? false;

            return canCreate;
        }

        public async Task<List<UserPermissions>> UserPermissions(string? userEmail = "")
        {
            User? userAuthenticated = await GetUserAuthenticated(userEmail);

            List<UserPermissions> userPermissions = [];

            if (userAuthenticated != null)
                userPermissions = _unitOfWork.UserPermissionsRepository.GetEntity().Where(x => x.Users.Id == userAuthenticated.Id).ToList();


            return userPermissions;
        }

        private async Task<User?> GetUserAuthenticated(string? userEmail)
        {
            string? email = userEmail;
            if (email.IsNullOrEmpty()) {
                email = _userClaimsService.GetUserEmail();
            }
            User? userAuthenticated = null;

            if (email != null)
                userAuthenticated = await _unitOfWork.UserRepository.GetByEmailAsync(email);
            return userAuthenticated;
        }
    }
}
