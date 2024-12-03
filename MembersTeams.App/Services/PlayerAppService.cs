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
    public class PlayerAppService : IPlayerAppService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;
        private readonly IPlayerProducer _playerProducer; 
        private readonly IUserPermissionsCachedRepository _userPermissionsCachedRepository;

        public PlayerAppService(INotificationContext notificationContext, IUnitOfWork unitOfWork, IPlayerService playerService, IMapper mapper, IPlayerProducer playerProducer, IUserPermissionsCachedRepository userPermissionsCachedRepository)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _playerService = playerService;
            _mapper = mapper;
            _playerProducer = playerProducer;
            _userPermissionsCachedRepository = userPermissionsCachedRepository;
        }

        #region Player
        public async Task<PlayerResponseDTO?> GetPlayer(long id)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            Player? player = await _unitOfWork.PlayerRepository.GetByIdAsync(id);

            return _mapper.Map<PlayerResponseDTO>(player);
        }

        public async Task<PlayerResponseDTO?> DeletePlayer(long id)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Delete)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_DELETE, string.Empty);
                return null;
            }

            Player? playerDeleted = await _playerService.DeletePlayer(id);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            if(playerDeleted != null)
                await _playerProducer.DeletePlayerProducer(playerDeleted.Id);

            return _mapper.Map<PlayerResponseDTO>(playerDeleted);
        }

        public async Task<PlayerResponseDTO?> CreatePlayer(CreatePlayerDTO playerBody)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Create)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CREATE, string.Empty);
                return null;
            }

            Player? player = await _playerService.CreatePlayer(playerBody);

            if (player == null)
            {
                _notificationContext.AddNotification(NotificationKeys.PlayerNotifications.ERROR_PLAYER_CREATED, string.Empty);
                return null;
            }

            player.PlayerResponsibles = await _playerService.CreatePlayerResponsible(playerBody, player);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            if (player != null)
                await _playerProducer.CreateUpdatePlayerProducer(player);

            return _mapper.Map<PlayerResponseDTO>(player);
        }

        public async Task<PlayerResponseDTO?> UpdatePlayer(UpdatePlayerDTO playerToUpdate)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Edit)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_EDIT, string.Empty);
                return null;
            }

            Player? player = null;

            if (playerToUpdate.Id != null)
                player = await _unitOfWork.PlayerRepository.GetById((long)playerToUpdate.Id);

            if (player == null)
            {
                _notificationContext.AddNotification(NotificationKeys.PlayerNotifications.PLAYER_DONT_EXITS, string.Empty);
                return null;
            }

            player = await _playerService.UpdatePlayer(playerToUpdate, player);

            if (player == null)
            {
                _notificationContext.AddNotification(NotificationKeys.PlayerNotifications.ERROR_PLAYER_UPDATED, string.Empty);
                return null;
            }

            player.PlayerResponsibles = await _playerService.UpdatePlayerResponsible(playerToUpdate, player);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            if (player != null)
                await _playerProducer.CreateUpdatePlayerProducer(player);

            return _mapper.Map<PlayerResponseDTO>(player);
        }

        public async Task<List<PlayerResponseDTO>?> SearchPlayersAsync(string? firstName, string? lastName, string? position)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            IEnumerable<Player>? player = await _unitOfWork.PlayerRepository.SearchPlayersAsync(firstName, lastName, position);

            return _mapper.Map<List<PlayerResponseDTO>>(player);
        }
        #endregion

        #region PlayerTransfer
        public async Task<List<PlayerTransferResponseDTO>?> GetPlayerTransfer(long id)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            List<PlayerTransfer>? playerTransfer = await _unitOfWork.PlayerTransferRepository.GetAllPlayerTransferAsync(id);

            return _mapper.Map<List<PlayerTransferResponseDTO>>(playerTransfer);
        }

        public async Task<PlayerTransferResponseDTO?> DeletePlayerTransfer(long id)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Delete)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_DELETE, string.Empty);
                return null;
            }

            PlayerTransfer? playerTransferDeleted = await _playerService.DeletePlayerTransfer(id);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<PlayerTransferResponseDTO>(playerTransferDeleted);
        }

        public async Task<PlayerTransferResponseDTO?> CreatePlayerTransfer(CreatePlayerTransferDTO playerTransferBody)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Create)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CREATE, string.Empty);
                return null;
            }

            PlayerTransfer? playerTransfer = await _playerService.CreatePlayerTransfer(playerTransferBody);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<PlayerTransferResponseDTO>(playerTransfer);
        }

        public async Task<PlayerTransferResponseDTO?> UpdatePlayerTransfer(UpdatePlayerTransferDTO playerTransferToUpdate)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Edit)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_EDIT, string.Empty);
                return null;
            }

            PlayerTransfer? playerTransfer = null;

            if (playerTransferToUpdate.Id != null)
                playerTransfer = await _unitOfWork.PlayerTransferRepository.GetById((long)playerTransferToUpdate.Id);

            if (playerTransfer == null)
            {
                _notificationContext.AddNotification(NotificationKeys.ClubMemberNotifications.CLUBMEMBER_DONT_EXITS, string.Empty);
                return null;
            }

            playerTransfer = await _playerService.UpdatePlayerTransfer(playerTransferToUpdate, playerTransfer);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<PlayerTransferResponseDTO>(playerTransfer);
        }
        #endregion

        #region PlayerContract
        public async Task<PlayerContractResponseDTO?> CreatePlayerContract(CreatePlayerContractDTO playerContractBody)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Create)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CREATE, string.Empty);
                return null;
            }

            List<PlayerContract>? listPlayerContract = await _unitOfWork.PlayerContractRepository.GetAllPlayerContractAsync(playerContractBody.PlayerId);

            if (listPlayerContract != null && listPlayerContract.Count > 1 || listPlayerContract != null && listPlayerContract.Count == 1 && listPlayerContract.FirstOrDefault()!.EndDate > DateTime.Now)
            {
                _notificationContext.AddNotification(NotificationKeys.PlayerContractNotifications.PLAYER_ALREADY_HAVE_ACTIVE_CONTRACT, string.Empty);
                return null;
            }

            PlayerContract? playerContract = await _playerService.CreatePlayerContract(playerContractBody);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<PlayerContractResponseDTO>(playerContract);
        }

        public async Task<List<PlayerContractResponseDTO>?> GetPlayerContract(long id)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            List<PlayerContract>? playerContract = await _unitOfWork.PlayerContractRepository.GetAllPlayerContractAsync(id);

            return _mapper.Map<List<PlayerContractResponseDTO>>(playerContract);
        }

        public async Task<PlayerContractResponseDTO?> DeletePlayerContract(long id)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Delete)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_DELETE, string.Empty);
                return null;
            }

            PlayerContract? playerContractDeleted = await _playerService.DeletePlayerContract(id);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<PlayerContractResponseDTO>(playerContractDeleted);
        }

        public async Task<PlayerContractResponseDTO?> UpdatePlayerContract(UpdatePlayerContractDTO playerContractToUpdate)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Edit)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_EDIT, string.Empty);
                return null;
            }

            PlayerContract? playerContract = null;

            if (playerContractToUpdate.Id != null)
                playerContract = await _unitOfWork.PlayerContractRepository.GetById((long)playerContractToUpdate.Id);

            if (playerContract == null)
            {
                _notificationContext.AddNotification(NotificationKeys.ClubMemberNotifications.CLUBMEMBER_DONT_EXITS, string.Empty);
                return null;
            }

            playerContract = await _playerService.UpdatePlayerContract(playerContractToUpdate, playerContract);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<PlayerContractResponseDTO>(playerContract);
        }
        #endregion

        #region PlayerPerformanceHistory
        public async Task<PlayerPerformanceHistorySimpleResponseDTO?> DeletePlayerPerformanceHistory(long id)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Delete)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_DELETE, string.Empty);
                return null;
            }

            PlayerPerformanceHistory? playerPerformanceHistoryDeleted = await _playerService.DeletePlayerPerformanceHistory(id);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<PlayerPerformanceHistorySimpleResponseDTO>(playerPerformanceHistoryDeleted);
        }

        public async Task<PlayerPerformanceHistorySimpleResponseDTO?> CreatePlayerPerformanceHistory(CreatePlayerPerformanceHistoryDTO playerPerformanceHistoryBody)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Create)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CREATE, string.Empty);
                return null;
            }

            PlayerPerformanceHistory? playerPerformanceHistory = await _playerService.CreatePlayerPerformanceHistory(playerPerformanceHistoryBody);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<PlayerPerformanceHistorySimpleResponseDTO>(playerPerformanceHistory);
        }

        public async Task<PlayerPerformanceHistorySimpleResponseDTO?> UpdatePlayerPerformanceHistory(UpdatePlayerPerformanceHistoryDTO playerPerformanceHistoryToUpdate)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Edit)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_EDIT, string.Empty);
                return null;
            }

            PlayerPerformanceHistory? playerPerformanceHistory = null;

            if (playerPerformanceHistoryToUpdate.Id != null)
                playerPerformanceHistory = await _unitOfWork.PlayerPerformanceHistoryRepository.GetById((long)playerPerformanceHistoryToUpdate.Id);

            if (playerPerformanceHistory == null)
            {
                _notificationContext.AddNotification(NotificationKeys.ClubMemberNotifications.CLUBMEMBER_DONT_EXITS, string.Empty);
                return null;
            }

            playerPerformanceHistory = await _playerService.UpdatePlayerPerformanceHistory(playerPerformanceHistoryToUpdate, playerPerformanceHistory);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return _mapper.Map<PlayerPerformanceHistorySimpleResponseDTO>(playerPerformanceHistory);
        }

        public async Task<PlayerPerformanceHistoryResponseDTO?> GetPlayerPerformanceHistory(long playerId, string season)
        {
            var response = await _userPermissionsCachedRepository.GetUserPermissionsByUserIdAsync();

            if (response != null && !response.Consult)
            {
                _notificationContext.AddNotification(NotificationKeys.CANT_CONSULT, string.Empty);
                return null;
            }

            List<PlayerPerformanceHistory>? allPlayerPerformanceHistoryOfPlayer = await _unitOfWork.PlayerPerformanceHistoryRepository.GetAllPlayerPerformanceHistoryForSeasonAsync(playerId, season);

            PlayerPerformanceHistoryResponseDTO responseHistory = new();

            if (allPlayerPerformanceHistoryOfPlayer != null && allPlayerPerformanceHistoryOfPlayer.Any())
            {
                responseHistory.Season = season;
                responseHistory.NumberOfGoals = allPlayerPerformanceHistoryOfPlayer.Sum(p => p.Goals);
                responseHistory.NumberOfAssists = allPlayerPerformanceHistoryOfPlayer.Sum(p => p.Assists);
                responseHistory.NumberOfMinutesPlayed = allPlayerPerformanceHistoryOfPlayer.Sum(p => p.MinutesPlayed);
                responseHistory.NumberOfMatchesPlayed = allPlayerPerformanceHistoryOfPlayer.Count;
                responseHistory.NumberOfYellowCards = allPlayerPerformanceHistoryOfPlayer.Sum(p => p.YellowCards);
                responseHistory.NumberOfRedCards = allPlayerPerformanceHistoryOfPlayer.Sum(p => p.RedCards);
            }

            return responseHistory;
        }
        #endregion

    }
}
