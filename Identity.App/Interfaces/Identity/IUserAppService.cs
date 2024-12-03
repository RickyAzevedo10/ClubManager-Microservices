using Identity.Domain.DTOs;
using Identity.Domain.Entities;

namespace Identity.App.Interfaces.Identity
{
    public interface IUserAppService
    {
        Task<UserResponseDTO?> Get(long id);
        Task<List<UserResponseDTO>?> GetAllFromInstitution(long idInstitution);
        Task<List<UserRoleResponseDTO>?> GetAllUserRoles();
        Task<List<UserPermissionResponseDTO>?> GetUserPermissions(long id);
        Task<UserResponseDTO?> Create(CreateUserDTO userCreate);
        Task<UserResponseDTO?> Update(UpdateUserDTO userCreateToUpdate);
        Task<UserResponseDTO?> Delete(long id);
        Task<UserLoginResponseDTO?> Login(UserLoginDTO user);
        Task<UserLoginResponseDTO?> Refresh(string refreshToken);
        Task<UserPermissionResponseDTO?> PutUserPermissions(UpdateUserPermissionsDTO userPermissionsCreate);
        Task<UserPermissionResponseDTO?> DeleteUserPermissions(long id);
        Task<User?> CreateUserAdmin(string abbreviation);
        Task<RecoverPasswordRequestResponseDTO?> RecoverPassword(RecoverPasswordRequestDTO request);
        Task<ResetPasswordResponseDTO?> ResetPassword(ResetPasswordDTO request);
    }
}
