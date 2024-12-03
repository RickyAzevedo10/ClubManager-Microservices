using Identity.Domain.Entities;

namespace Identity.App.Interfaces.Infrastructure
{
    public interface IAuthorizationService
    {
        Task<bool> CanEdit(string? userEmail = "");
        Task<bool> CanConsult(string? userEmail = "");
        Task<bool> CanDelete(string? userEmail = "");
        Task<bool> CanCreate(string? userEmail = "");
        Task<List<UserPermissions>> UserPermissions(string? userEmail = "");
    }
}
