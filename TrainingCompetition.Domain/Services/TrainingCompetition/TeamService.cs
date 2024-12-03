using Contracts;
using TrainingCompetition.Domain.Entities;
using TrainingCompetition.Domain.Interfaces.Identity;
using TrainingCompetition.Domain.Interfaces.Repositories;

namespace MembersTeams.Domain.Services.Identity
{
    public class TeamService : ITeamService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Team?> DeleteTeam(Team? team)
        {
            _unitOfWork.TeamRepository.Delete(team);
            return team;
        }

        public async Task<Team?> CreateTeam(CreateUpdateTeam teamBody)
        {
            Team team = new();
            team.SetMale(teamBody.Male);
            team.SetName(teamBody.Name);
            team.SetFemale(teamBody.Female);

            return await _unitOfWork.TeamRepository.AddAsync(team);
        }

        public async Task<Team?> UpdateTeam(CreateUpdateTeam teamToUpdate, Team team)
        {
            team.SetMale(teamToUpdate.Male);
            team.SetName(teamToUpdate.Name);
            team.SetFemale(teamToUpdate.Female);

            _unitOfWork.TeamRepository.Update(team);
            return team;
        }

    }
}
