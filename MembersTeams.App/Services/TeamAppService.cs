using AutoMapper;
using MembersTeams.Application.Interfaces;
using MembersTeams.Application.Interfaces.Infrastructure;
using MembersTeams.Application.Interfaces.Producers;
using MembersTeams.Domain.DTOs;
using MembersTeams.Domain.Entities;
using MembersTeams.Domain.Interfaces;
using MembersTeams.Domain.Interfaces.Identity;
using MembersTeams.Domain.Interfaces.Repositories;
using static MembersTeams.Domain.Constants.Constants;

namespace MembersTeams.Application.Services
{
    public class TeamAppService : ITeamAppService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;
        private readonly ITeamProducer _teamProducer;
        private readonly IUserPermissionsCachedRepository _userPermissionsCachedRepository;

        public TeamAppService(INotificationContext notificationContext, IUnitOfWork unitOfWork, ITeamService teamService, IMapper mapper, ITeamProducer teamProducer, IUserPermissionsCachedRepository userPermissionsCachedRepository)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _teamService = teamService;
            _mapper = mapper;
            _teamProducer = teamProducer;
            _userPermissionsCachedRepository = userPermissionsCachedRepository;
        }

        public async Task<List<TeamResponseDTO>?> GetTeams()
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            IEnumerable<Team>? allTeams = await _unitOfWork.TeamRepository.GetAllAsync();

            return _mapper.Map<List<TeamResponseDTO>>(allTeams);
        }

        public async Task<List<TeamResponseDTO>?> GetAllPlayersFromTeam(long teamId)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            List<Team>? allTeams = await _unitOfWork.TeamRepository.GetAllPlayerFromTeamAsync(teamId);

            return _mapper.Map<List<TeamResponseDTO>>(allTeams);
        }

        public async Task<TeamResponseDTO?> DeleteTeam(long id)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Delete)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_DELETE, string.Empty);
                return null;
            }

            Team? teamDeleted = await _teamService.DeleteTeam(id);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            if (teamDeleted != null)
                await _teamProducer.DeleteTeamProducer(teamDeleted.Id);

            return _mapper.Map<TeamResponseDTO>(teamDeleted);
        }

        public async Task<TeamResponseDTO?> CreateTeam(CreateTeamDTO teamBody)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Create)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CREATE, string.Empty);
                return null;
            }

            Team? team = await _teamService.CreateTeam(teamBody);
            if (_notificationContext.HasNotifications())
            {
                return null;
            }

            team.TeamCoaches = await _teamService.CreateTeamCoach(teamBody.TeamCoachDTO, team);
            team.TeamPlayers = await _teamService.CreateTeamPlayer(teamBody.PlayerId, team);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            if (team != null)
                await _teamProducer.CreateUpdateTeamProducer(team);

            return _mapper.Map<TeamResponseDTO>(team);
        }

        public async Task<TeamResponseDTO?> UpdateTeam(UpdateTeamDTO teamToUpdate)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Edit)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_EDIT, string.Empty);
                return null;
            }

            Team? team = null;

            if (teamToUpdate.Id != null)
                team = await _unitOfWork.TeamRepository.GetById((long)teamToUpdate.Id);

            if (team == null)
            {
                _notificationContext.AddNotification(NotificationKeys.TeamNotifications.TEAM_DONT_EXITS, string.Empty);
                return null;
            }

            team = await _teamService.UpdateTeam(teamToUpdate, team);

            if (team == null || _notificationContext.HasNotifications())
                return null;

            team.TeamCoaches = await _teamService.UpdateTeamCoaches(teamToUpdate, team);
            team.TeamPlayers = await _teamService.UpdateTeamPlayers(teamToUpdate, team);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            if (team != null)
                await _teamProducer.CreateUpdateTeamProducer(team);

            return _mapper.Map<TeamResponseDTO>(team);
        }
    }
}
