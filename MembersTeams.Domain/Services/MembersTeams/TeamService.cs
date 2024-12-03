using FluentValidation;
using MembersTeams.Domain.DTOs;
using MembersTeams.Domain.Entities;
using MembersTeams.Domain.Interfaces;
using MembersTeams.Domain.Interfaces.Identity;
using MembersTeams.Domain.Interfaces.Repositories;
using static MembersTeams.Domain.Constants.Constants;

namespace MembersTeams.Domain.Services.Identity
{
    public class TeamService : ITeamService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityValidationService _entityValidationService;
        private readonly IValidator<CreateTeamDTO> _createTeamValidator;
        private readonly IValidator<UpdateTeamDTO> _updateTeamValidator;

        public TeamService(INotificationContext notificationContext, IUnitOfWork unitOfWork,
            IEntityValidationService entityValidationService, IValidator<CreateTeamDTO> createTeamValidator, IValidator<UpdateTeamDTO> updateTeamValidator)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _entityValidationService = entityValidationService;
            _createTeamValidator = createTeamValidator;
            _updateTeamValidator = updateTeamValidator;
        }

        public async Task<Team?> DeleteTeam(long id)
        {
            Team? team = await _unitOfWork.TeamRepository.GetById(id);

            if (team == null)
            {
                _notificationContext.AddNotification(NotificationKeys.TeamNotifications.TEAM_DONT_EXITS, string.Empty);
                return null;
            }

            _unitOfWork.TeamRepository.Delete(team);
            return team;
        }

        public async Task<Team?> CreateTeam(CreateTeamDTO teamBody)
        {
            bool validationResult = _entityValidationService.Validate(_createTeamValidator, teamBody);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.TeamNotifications.ERROR_TEAM_CREATED, string.Empty);
                return null;
            }

            Team team = new();
            team.SetMale(teamBody.Male);
            team.SetName(teamBody.Name);
            team.SetFemale(teamBody.Female);
            team.SetClubId(teamBody.ClubId);
            team.SetTeamCategoryId(teamBody.TeamCategoryId);

            return await _unitOfWork.TeamRepository.AddAsync(team);
        }

        public async Task<List<TeamCoach>?> CreateTeamCoach(List<CreateTeamCoachDTO> teamCoachBody, Team team)
        {
            List<TeamCoach> teamCoachs = [];

            foreach (var item in teamCoachBody)
            {
                var user = 1;
                //TODO
                //User user = await _unitOfWork.UserRepository.GetById(item.UserId);
                if (user == null)
                {
                    _notificationContext.AddNotification(NotificationKeys.TeamNotifications.ERROR_TEAM_CREATED, string.Empty);
                    return null;
                }
                TeamCoach teamCoach = new();
                teamCoach.SetTeamId(team.Id);
                teamCoach.SetUserId(item.UserId);
                teamCoach.SetIsHeadCoach(item.IsHeadCoach);

                await _unitOfWork.TeamCoachRepository.AddAsync(teamCoach);
                teamCoachs.Add(teamCoach);
            }
            return teamCoachs;
        }

        public async Task<List<TeamPlayer>?> CreateTeamPlayer(List<long> teamBody, Team team)
        {
            List<TeamPlayer> teamPlayers = [];

            foreach (var playerId in teamBody)
            {
                Player player = await _unitOfWork.PlayerRepository.GetById(playerId);
                if (player == null)
                {
                    _notificationContext.AddNotification(NotificationKeys.TeamNotifications.ERROR_TEAM_CREATED, string.Empty);
                    return null;
                }
                TeamPlayer teamPlayer = new();
                teamPlayer.SetTeamId((int)team.Id);
                teamPlayer.SetPlayerId((int)playerId);

                await _unitOfWork.TeamPlayerRepository.AddAsync(teamPlayer);
                teamPlayers.Add(teamPlayer);
            }
            return teamPlayers;
        }

        public async Task<Team?> UpdateTeam(UpdateTeamDTO teamToUpdate, Team team)
        {
            bool validationResult = _entityValidationService.Validate(_updateTeamValidator, teamToUpdate);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.TeamNotifications.ERROR_TEAM_UPDATED, string.Empty);
                return null;
            }
            team.SetMale(teamToUpdate.Male);
            team.SetName(teamToUpdate.Name);
            team.SetFemale(teamToUpdate.Female);
            team.SetClubId(teamToUpdate.ClubId);
            team.SetTeamCategoryId(teamToUpdate.TeamCategoryId);

            _unitOfWork.TeamRepository.Update(team);
            return team;
        }

        public async Task<List<TeamCoach>?> UpdateTeamCoaches(UpdateTeamDTO teamToUpdate, Team team)
        {
            List<TeamCoach> teamCoaches = [];

            foreach (var item in teamToUpdate.TeamCoachDTO)
            {
                if(item.Id != 0)
                {
                    //update if exists
                    TeamCoach teamCoach = await _unitOfWork.TeamCoachRepository.GetById(item.Id);
                    if (teamCoach != null)
                    {
                        teamCoach.SetTeamId(team.Id);
                        teamCoach.SetUserId(item.UserId);
                        teamCoach.SetIsHeadCoach(item.IsHeadCoach);
                        _unitOfWork.TeamCoachRepository.Update(teamCoach);
                        teamCoaches.Add(teamCoach);
                    }
                } else
                {
                    //create a new one
                    CreateTeamCoachDTO teamCoachDTO = new()
                    {
                        UserId = item.UserId,
                        IsHeadCoach = item.IsHeadCoach
                    };
                    List<CreateTeamCoachDTO> listTeamCoachDTO = [teamCoachDTO];
                    List<TeamCoach>? teamCoachCreated = await CreateTeamCoach(listTeamCoachDTO, team);
                    if(teamCoachCreated != null && teamCoachCreated.Any())
                        teamCoaches.Add(teamCoachCreated[1]);
                }
            }
            return teamCoaches;
            //await _unitOfWork.TeamCoachRepository.DeleteAllAsync();
            //return await CreateTeamCoach(teamToUpdate.TeamCoachDTO, team);
        }

        public async Task<List<TeamPlayer>?> UpdateTeamPlayers(UpdateTeamDTO teamToUpdate, Team team)
        {
            List<TeamPlayer> teamPlayers = [];

            foreach (var item in teamToUpdate.TeamPlayers)
            {
                if (item.Id != 0)
                {
                    //update if exists
                    TeamPlayer teamPlayer = await _unitOfWork.TeamPlayerRepository.GetById(item.Id);
                    if (teamPlayer != null)
                    {
                        teamPlayer.SetTeamId(team.Id);
                        teamPlayer.SetPlayerId(item.PlayerId);
                        _unitOfWork.TeamPlayerRepository.Update(teamPlayer);
                        teamPlayers.Add(teamPlayer);
                    }
                }
                else
                {
                    //create a new one
                    List<long> playerdIds = [item.PlayerId];
                    List<TeamPlayer>? teamPlayerCreated = await CreateTeamPlayer(playerdIds, team);
                    if (teamPlayerCreated != null && teamPlayerCreated.Any())
                        teamPlayers.Add(teamPlayerCreated[1]);
                }
            }
            return teamPlayers;
            //await _unitOfWork.TeamCoachRepository.DeleteAllAsync();
            //return await CreateTeamPlayer(teamToUpdate.PlayerId, team);
        }

    }
}
