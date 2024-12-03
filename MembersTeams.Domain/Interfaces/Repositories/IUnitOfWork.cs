using MembersTeams.Domain.Entities;
using MembersTeams.Domain.Interfaces.Repositories.Identity;

namespace MembersTeams.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
        
        //MembersTeams
        IPlayerRepository PlayerRepository { get; }
        IBaseRepository<PlayerCategory> PlayerCategoryRepository { get; }
        IPlayerPerformanceHistoryRepository PlayerPerformanceHistoryRepository { get; }
        IClubMemberRepository ClubMemberRepository { get; }
        IMinorClubMemberRepository MinorClubMemberRepository { get; }
        IBaseRepository<PlayerResponsible> PlayerResponsibleRepository { get; }
        IPlayerContractRepository PlayerContractRepository { get; }
        IPlayerTransferRepository PlayerTransferRepository { get; }
        ITeamRepository TeamRepository { get; }
        IBaseRepository<TeamCategory> TeamCategoryRepository { get; }
        IBaseRepository<TeamPlayer> TeamPlayerRepository { get; }
        IBaseRepository<TeamCoach> TeamCoachRepository { get; }
        IUserClubMemberRepository UserClubMemberRepository { get; }
        IBaseRepository<User> UserRepository { get; }
        IBaseRepository<Institution> InstitutionRepository { get; }
    }
}