using AutoMapper;
using Identity.App.Interfaces.Identity;
using Identity.App.Interfaces.Infrastructure;
using Identity.Application.Interfaces.Producers;
using Identity.Domain.DTOs;
using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using Identity.Domain.Interfaces.Identity;
using Identity.Domain.Interfaces.Persistence.CachedRepositories;
using Identity.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using static Identity.Domain.Constants.Constants;

namespace Identity.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IRefreshTokenCachedRepository _refreshTokenCachedRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUserProducer _userCreateProducer;
        private readonly IUserPermissionsCachedRepository _userPermissionsCachedRepository;

        public UserAppService(INotificationContext notificationContext, IUnitOfWork unitOfWork, IAuthorizationService authorizationService,
            IUserService userService, IAuthenticationService authenticationService, IMapper mapper, IConfiguration configuration, IRefreshTokenCachedRepository refreshTokenCachedRepository,
            IUserProducer userCreateProducer, IUserPermissionsCachedRepository userPermissionsCachedRepository)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _authorizationService = authorizationService;
            _userService = userService;
            _authenticationService = authenticationService;
            _mapper = mapper;
            _configuration = configuration;
            _refreshTokenCachedRepository = refreshTokenCachedRepository;
            _userCreateProducer = userCreateProducer;
            _userPermissionsCachedRepository = userPermissionsCachedRepository;
        }

        public async Task<UserResponseDTO?> Get(long id)
        {
            bool canConsult = await _authorizationService.CanConsult();

            if (!canConsult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            User? user = await _userService.Get(id);

            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<List<UserResponseDTO>?> GetAllFromInstitution(long idInstitution)
        {
            bool canConsult = await _authorizationService.CanConsult();

            if (!canConsult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            List<User>? usersInstitution = await _userService.GetAllUsersFromInstitution(idInstitution);

            return _mapper.Map<List<UserResponseDTO>>(usersInstitution);
        }

        public async Task<List<UserRoleResponseDTO>?> GetAllUserRoles()
        {
            bool canConsult = await _authorizationService.CanConsult();

            if (!canConsult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            IEnumerable<UserRoles>? userRoles = await _unitOfWork.UserRolesRepository.GetAllAsync();

            return _mapper.Map<List<UserRoleResponseDTO>>(userRoles);
        }

        public async Task<List<UserPermissionResponseDTO>?> GetUserPermissions(long id)
        {
            bool canConsult = await _authorizationService.CanConsult();

            if (!canConsult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            List<UserPermissions>? userPermissions = await _unitOfWork.UserPermissionsRepository.GetUserPermissions(id);

            return _mapper.Map<List<UserPermissionResponseDTO>>(userPermissions);
        }

        public async Task<UserResponseDTO?> Create(CreateUserDTO userCreate)
        {
            bool canCreate = await _authorizationService.CanCreate();

            if (!canCreate)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CREATE, string.Empty);
                return null;
            }

            User? user = await _unitOfWork.UserRepository.GetByEmailAsync(userCreate.Email);

            if (user != null)
            {
                _notificationContext.AddNotification(NotificationKeys.UserNotifications.USER_ALREADY_EXITS, string.Empty);
                return null;
            }

            CreateUserPermissionsDTO createUserPermissionsDTO = new()
            {
                Edit = userCreate.Edit,
                Consult = userCreate.Consult,
                Delete = userCreate.Delete,
                Create = userCreate.Create,
            };

            UserPermissions? userPermissions = await _userService.CreateUserPermissions(createUserPermissionsDTO);

            user = await _userService.Create(userCreate);

            if (userPermissions != null && user != null)
                user.UserPermission = userPermissions;

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            if (user != null)
                await _userCreateProducer.CreateUpdateUserProducer(user);

            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<UserResponseDTO?> Update(UpdateUserDTO userCreateToUpdate)
        {
            bool canEdit = await _authorizationService.CanEdit();

            if (!canEdit)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_EDIT, string.Empty);
                return null;
            }

            User? user = await _unitOfWork.UserRepository.GetByIdAsync(userCreateToUpdate.Id);

            if (user == null)
            {
                _notificationContext.AddNotification(NotificationKeys.UserNotifications.USER_DONT_EXITS, string.Empty);
                return null;
            }

            user = await _userService.Update(userCreateToUpdate, user);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }
            
            if(user != null)
                await _userCreateProducer.CreateUpdateUserProducer(user);

            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<UserResponseDTO?> Delete(long id)
        {
            bool canDelete = await _authorizationService.CanDelete();

            if (!canDelete)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_DELETE, string.Empty);
                return null;
            }

            User? userDeleted = await _userService.Delete(id);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }
            if(userDeleted != null)
                await _userCreateProducer.DeleteUserProducer(userDeleted.Id);

            return _mapper.Map<UserResponseDTO>(userDeleted);
        }

        public async Task<UserLoginResponseDTO?> Login(UserLoginDTO user)
        {
            User? existentUser = await _unitOfWork.UserRepository.GetByEmailAsync(user.Email);

            if (existentUser == null)
            {
                _notificationContext.AddNotification(NotificationKeys.UserNotifications.USER_DONT_EXITS, string.Empty);
                return null;
            }

            if (!existentUser.ValidatePassword(user.Password))
            {
                _notificationContext.AddNotification(NotificationKeys.UserNotifications.INVALID_USER_CREDENTIALS, string.Empty);
                return null;
            }
            UserCacheInformationDTO userInformation = new(existentUser.Id, existentUser.Email, existentUser.UserRole.Name);
            UserPermissionsCacheInformationDTO userPermissions = new(existentUser.Id, existentUser.Email, existentUser.UserPermission.Edit, existentUser.UserPermission.Delete,
                existentUser.UserPermission.Consult, existentUser.UserPermission.Create);

            var refreshToken = _authenticationService.GenerateRefreshToken();
            var token = _authenticationService.GenerateToken(userInformation);
            int expiresHoursRefreshToken = int.Parse(_configuration.GetSection("RefreshToken:ExpiresHours").Value!);

            await _refreshTokenCachedRepository.SetAsync(refreshToken, userInformation, expiresHoursRefreshToken);
            await _userPermissionsCachedRepository.SetAsync(existentUser.Id, userPermissions);

            existentUser.UpdateLastAccessDate();

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return new()
            {
                Token = token,
                RefreshToken = refreshToken
            };
        }

        public async Task<UserLoginResponseDTO?> Refresh(string refreshToken)
        {
            var user = await _refreshTokenCachedRepository.GetUserClaimsByRefreshTokenAsync(refreshToken);
            if (user == null)
            {
                _notificationContext.AddNotification(NotificationKeys.UserNotifications.INVALID_REFRESH_TOKEN, string.Empty);
                return null;
            }

            var newRefreshToken = _authenticationService.GenerateRefreshToken();
            var newToken = _authenticationService.GenerateToken(user);
            int expiresHoursRefreshToken = int.Parse(_configuration.GetSection("RefreshToken:ExpiresHours").Value!);

            await _refreshTokenCachedRepository.RemoveAsync(refreshToken);
            await _refreshTokenCachedRepository.SetAsync(newRefreshToken, user, expiresHoursRefreshToken);

            return new()
            {
                Token = newToken,
                RefreshToken = newRefreshToken
            };
        }

        public async Task<UserPermissionResponseDTO?> PutUserPermissions(UpdateUserPermissionsDTO userPermissionsCreate)
        {
            bool canEdit = await _authorizationService.CanEdit();

            if (!canEdit)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_EDIT, string.Empty);
                return null;
            }

            User? user = await _unitOfWork.UserRepository.GetByIdAsync(userPermissionsCreate.UserId);
            UserPermissions? userPermissions = new();
            if (user != null)
                userPermissions = await _userService.UpdateUserPermissions(userPermissionsCreate, user.UserPermission);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }
            UserPermissionsCacheInformationDTO newUserPermissions = new(userPermissionsCreate.UserId, user.Email, userPermissions.Edit, userPermissions.Delete,
                userPermissions.Consult, userPermissions.Create);

            await _userPermissionsCachedRepository.RemoveAsync(userPermissionsCreate.UserId);
            await _userPermissionsCachedRepository.SetAsync(userPermissionsCreate.UserId, newUserPermissions);

            return _mapper.Map<UserPermissionResponseDTO>(userPermissions);
        }

        public async Task<UserPermissionResponseDTO?> DeleteUserPermissions(long id)
        {
            bool canDelete = await _authorizationService.CanDelete();

            if (!canDelete)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_DELETE, string.Empty);
                return null;
            }

            UserPermissions? userPermissions = await _userService.DeleteUserPermissions(id);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            await _userPermissionsCachedRepository.RemoveAsync(userPermissions.Users.Id);

            return _mapper.Map<UserPermissionResponseDTO>(userPermissions);
        }

        public async Task<User?> CreateUserAdmin(string abbreviation)
        {
            CreateUserPermissionsDTO createUserPermissionsDTO = new()
            {
                Consult = true,
                Edit = true,
                Delete = true,
                Create = true
            };

            UserPermissions? userPermissions = await _userService.CreateUserPermissions(createUserPermissionsDTO);

            CreateUserDTO userAdmin = new()
            {
                Email = "admin" + abbreviation + "@gmail.com",
                Password = "Admin123",
                Username = "Admin",
                RoleId = 1,
                Consult = true,
                Edit = true,
                Delete = true,
                Create = true,
            };

            User? user = await _userService.Create(userAdmin);

            if (userPermissions != null && user != null)
                user.UserPermission = userPermissions;

            return user;
        }

        public async Task<RecoverPasswordRequestResponseDTO?> RecoverPassword(RecoverPasswordRequestDTO request)
        {
            User? user = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                _notificationContext.AddNotification(NotificationKeys.UserNotifications.USER_DONT_EXITS, string.Empty);
                return null;
            }
            UserCacheInformationDTO userInformation = new(user.Id, user.Email, user.UserRole.Name);

            var resetToken = _authenticationService.GenerateToken(userInformation);
            _userService.UpdatePasswordResetToken(user, resetToken);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            RecoverPasswordRequestResponseDTO response = new()
            {
                Email = request.Email,
                Token = resetToken
            };

            return response;
        }

        public async Task<ResetPasswordResponseDTO?> ResetPassword(ResetPasswordDTO request)
        {
            User? user = await _unitOfWork.UserRepository.GetByPasswordResetTokenAsync(request.Token);
            if (user == null)
            {
                _notificationContext.AddNotification(NotificationKeys.UserNotifications.USER_DONT_EXITS, string.Empty);
                return null;
            }

            if (user.PasswordResetTokenExpire < DateTime.Now)
            {
                _notificationContext.AddNotification(NotificationKeys.UserNotifications.INVALID_REFRESH_TOKEN, string.Empty);
                return null;
            }

            //update password
            user = await _userService.UpdatePassword(user, request);

            if (user == null || !await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }
            else
            {
                ResetPasswordResponseDTO response = new() { NewPassword = request.NewPassword, Message = "Password has been changed" };
                return response;
            }
        }
    }
}
