namespace MembersTeams.Application.Interfaces.Infrastructure
{
    public interface IUserClaimsService
    {
        string? GetUserEmail();
        long? GetUserId();
    }
}
