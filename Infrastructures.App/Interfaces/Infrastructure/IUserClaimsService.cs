namespace Infrastructures.Application.Services.Infrastructure
{
    public interface IUserClaimsService
    {
        string? GetUserEmail();
        long? GetUserId();
    }
}
