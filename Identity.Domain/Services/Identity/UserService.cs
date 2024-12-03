using FluentValidation;
using Identity.Domain.DTOs;
using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using Identity.Domain.Interfaces.Identity;
using Identity.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using static Identity.Domain.Constants.Constants;

namespace Identity.Domain.Services.Identity
{
    public class UserService : IUserService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityValidationService _entityValidationService;
        private readonly IValidator<CreateUserDTO> _userValidator;
        private readonly IValidator<UpdateUserDTO> _userUpdateValidator;
        private readonly IValidator<UpdateUserPermissionsDTO> _userUpdatePermissionsValidator;
        private readonly IValidator<CreateUserPermissionsDTO> _userPermissionsValidator;
        private readonly IValidator<ResetPasswordDTO> _passwordValidator;
        private readonly IConfiguration _configuration;

        public UserService(INotificationContext notificationContext, IUnitOfWork unitOfWork, IConfiguration configuration, 
            IEntityValidationService entityValidationService, IValidator<CreateUserDTO> userValidator, IValidator<CreateUserPermissionsDTO> userPermissionsValidator,
            IValidator<UpdateUserPermissionsDTO> userUpdatePermissionsValidator, IValidator<ResetPasswordDTO> passwordValidator, IValidator<UpdateUserDTO> userUpdateValidator)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _entityValidationService = entityValidationService;
            _userValidator = userValidator;
            _userPermissionsValidator = userPermissionsValidator;
            _userUpdatePermissionsValidator = userUpdatePermissionsValidator;
            _passwordValidator = passwordValidator;
            _userUpdateValidator = userUpdateValidator;
        }

        public async Task<User?> Get(long id)
        {
            User? user = await _unitOfWork.UserRepository.GetByIdAsync(id);

            if (user == null)
            {
                _notificationContext.AddNotification(NotificationKeys.UserNotifications.USER_DONT_EXITS, string.Empty);
                return null;
            }

            return user;
        }

        public async Task<List<User>?> GetAllUsersFromInstitution(long idInstitution)
        {
            List<User>? users = await _unitOfWork.UserRepository.GetAllUsersFromInstitution(idInstitution);

            if (users == null)
            {
                _notificationContext.AddNotification(NotificationKeys.UserNotifications.USER_DONT_EXITS, string.Empty);
                return null;
            }

            return users;
        }

        public async Task<User?> Create(CreateUserDTO createUser)
        {
            bool validationResult = _entityValidationService.Validate(_userValidator, createUser);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.UserNotifications.ERROR_USER_CREATED, string.Empty);
                return null;
            }

            User user = new(createUser);

            return await _unitOfWork.UserRepository.AddAsync(user);
        }

        public async Task<User?> Update(UpdateUserDTO userToUpdate, User user)
        {
            bool validationResult = _entityValidationService.Validate(_userUpdateValidator, userToUpdate);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.UserNotifications.ERROR_USER_UPDATED, string.Empty);
                return null;
            }

            user.Email = userToUpdate.Email;
            user.Username = userToUpdate.Username;
            user.SetPassword(userToUpdate.Password);
            user.SetPhoneNumber(userToUpdate.PhoneNumber);
            user.SetRoleId(userToUpdate.RoleId);

            _unitOfWork.UserRepository.Update(user);
            return user;
        }

        public async Task<User?> Delete(long id)
        {
            User? user = await _unitOfWork.UserRepository.GetByIdAsync(id);

            if (user == null)
            {
                _notificationContext.AddNotification(NotificationKeys.UserNotifications.USER_DONT_EXITS, string.Empty);
                return null;
            }

            _unitOfWork.UserRepository.Delete(user);
            return user;
        }

        public void UpdateRefreshToken(User user, string refreshToken)
        {
            int expiresHoursRefreshToken = int.Parse(_configuration.GetSection("RefreshToken:ExpiresHours").Value!);
            user.UpdateRefreshToken(refreshToken, expiresHoursRefreshToken);
            _unitOfWork.UserRepository.Update(user);
        }

        public async Task<UserPermissions?> CreateUserPermissions(CreateUserPermissionsDTO createUserPermissions)
        {
            bool validationResult = _entityValidationService.Validate(_userPermissionsValidator, createUserPermissions);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.UserNotifications.ERROR_USER_PERMISSIONS_CREATED, string.Empty);
                return null;
            }

            UserPermissions userPermissions = new(createUserPermissions.Edit, createUserPermissions.Create, createUserPermissions.Delete, createUserPermissions.Consult);

            return await _unitOfWork.UserPermissionsRepository.AddAsync(userPermissions);
        }

        public async Task<UserPermissions?> UpdateUserPermissions(UpdateUserPermissionsDTO updateUserPermissions, UserPermissions userPermissions)
        {
            bool validationResult = _entityValidationService.Validate(_userUpdatePermissionsValidator, updateUserPermissions);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.UserNotifications.ERROR_USER_PERMISSIONS_CREATED, string.Empty);
                return null;
            }

            userPermissions.Edit = updateUserPermissions.Edit;
            userPermissions.Create = updateUserPermissions.Create;
            userPermissions.Delete = updateUserPermissions.Delete;
            userPermissions.Consult = updateUserPermissions.Consult;

            _unitOfWork.UserPermissionsRepository.Update(userPermissions);

            return userPermissions;
        }

        public async Task<UserPermissions?> DeleteUserPermissions(long id)
        {
            UserPermissions? userPermissions = await _unitOfWork.UserPermissionsRepository.GetById(id);

            if (userPermissions == null)
            {
                _notificationContext.AddNotification(NotificationKeys.UserNotifications.USER_DONT_EXITS, string.Empty);
                return null;
            }

            _unitOfWork.UserPermissionsRepository.Delete(userPermissions);
            return userPermissions;
        }

        public void UpdatePasswordResetToken(User user, string passwordResetToken)
        {
            int expiresHoursRefreshToken = int.Parse(_configuration.GetSection("RefreshToken:ExpiresHours").Value!);
            user.SetPasswordResetToken(passwordResetToken);
            user.SetPasswordResetTokenExpire(expiresHoursRefreshToken);

            _unitOfWork.UserRepository.Update(user);
        }

        public async Task<User?> UpdatePassword(User user, ResetPasswordDTO request)
        {
            bool validationResult = _entityValidationService.Validate(_passwordValidator, request);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.UserNotifications.INVALID_USER_CREDENTIALS, string.Empty);
                return null;
            }

            user.SetPasswordResetToken("");
            user.SetPasswordResetTokenExpire(0);
            user.SetPassword(request.NewPassword);

            _unitOfWork.UserRepository.Update(user);
            return user;
        }
    }
}
