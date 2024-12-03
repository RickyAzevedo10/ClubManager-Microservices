using MembersTeams.Domain.Interfaces;
using MembersTeams.Domain.Interfaces.Identity;
using MembersTeams.Domain.Interfaces.Repositories;
using FluentValidation;
using MembersTeams.Domain.DTOs;
using MembersTeams.Domain.Entities;
using Microsoft.Extensions.Configuration;
using static MembersTeams.Domain.Constants.Constants;

namespace MembersTeams.Domain.Services.Identity
{
    public class PlayerService : IPlayerService
    {
        private readonly INotificationContext _notificationContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityValidationService _entityValidationService;
        private readonly IConfiguration _configuration;
        private readonly IValidator<CreatePlayerTransferDTO> _createPlayerTransferValidator;
        private readonly IValidator<UpdatePlayerTransferDTO> _updatePlayerTransferValidator;
        private readonly IValidator<CreatePlayerContractDTO> _createPlayerContractValidator;
        private readonly IValidator<UpdatePlayerContractDTO> _updatePlayerContractValidator;
        private readonly IValidator<CreatePlayerPerformanceHistoryDTO> _createPlayerPerformanceHistoryValidator;
        private readonly IValidator<UpdatePlayerPerformanceHistoryDTO> _updatePlayerPerformanceHistoryValidator;
        private readonly IValidator<CreatePlayerDTO> _createPlayerValidator;
        private readonly IValidator<UpdatePlayerDTO> _updatePlayerValidator;
        private readonly IValidator<CreateResponsiblePlayerDTO> _createResponsiblePlayerValidator;
        private readonly IValidator<UpdateResponsiblePlayerDTO> _updateResponsiblePlayerValidator;

        public PlayerService(INotificationContext notificationContext, IUnitOfWork unitOfWork, IConfiguration configuration, 
            IEntityValidationService entityValidationService, IValidator<CreatePlayerTransferDTO> createPlayerTransferValidator, 
            IValidator<UpdatePlayerTransferDTO> updatePlayerTransferValidator, IValidator<CreatePlayerContractDTO> createPlayerContractValidator, 
            IValidator<UpdatePlayerContractDTO> updatePlayerContractValidator, IValidator<CreatePlayerPerformanceHistoryDTO> createPlayerPerformanceHistoryValidator,
            IValidator<UpdatePlayerPerformanceHistoryDTO> updatePlayerPerformanceHistoryValidator, IValidator<CreatePlayerDTO> createPlayerValidator, 
            IValidator<CreateResponsiblePlayerDTO> createResponsiblePlayerValidator, IValidator<UpdatePlayerDTO> updatePlayerValidator, IValidator<UpdateResponsiblePlayerDTO> updateResponsiblePlayerValidator)
        {
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _entityValidationService = entityValidationService;
            _createPlayerTransferValidator = createPlayerTransferValidator;
            _updatePlayerTransferValidator = updatePlayerTransferValidator;
            _createPlayerContractValidator = createPlayerContractValidator;
            _updatePlayerContractValidator = updatePlayerContractValidator;
            _createPlayerPerformanceHistoryValidator = createPlayerPerformanceHistoryValidator;
            _updatePlayerPerformanceHistoryValidator = updatePlayerPerformanceHistoryValidator;
            _createPlayerValidator = createPlayerValidator;
            _createResponsiblePlayerValidator = createResponsiblePlayerValidator;
            _updatePlayerValidator = updatePlayerValidator;
            _updateResponsiblePlayerValidator = updateResponsiblePlayerValidator;
        }

        #region Player
        public async Task<Player?> DeletePlayer(long id)
        {
            Player? player = await _unitOfWork.PlayerRepository.GetById(id);

            if (player == null)
            {
                _notificationContext.AddNotification(NotificationKeys.PlayerNotifications.PLAYER_DONT_EXITS, string.Empty);
                return null;
            }

            _unitOfWork.PlayerRepository.Delete(player);
            return player;
        }

        public async Task<Player?> CreatePlayer(CreatePlayerDTO playerBody)
        {
            bool validationResult = _entityValidationService.Validate(_createPlayerValidator, playerBody);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.PlayerNotifications.ERROR_PLAYER_CREATED, string.Empty);
                return null;
            }

            Player player = new();
            player.SetDateOfBirth(playerBody.DateOfBirth);
            player.SetPosition(playerBody.Position);
            player.SetNationality(playerBody.Nationality);
            player.SetPreferredFoot(playerBody.PreferredFoot);
            player.SetLastName(playerBody.LastName);
            player.SetFirstName(playerBody.FirstName);
            player.SetHeight(playerBody.Height);
            player.SetWeight(playerBody.Weight);
            player.SetPlayerCategoryId(playerBody.PlayerCategoryId);

            return await _unitOfWork.PlayerRepository.AddAsync(player);
        }

        public async Task<List<PlayerResponsible>?> CreatePlayerResponsible(CreatePlayerDTO playerBody, Player player)
        {
            List<PlayerResponsible>? listPlayerResponsibles = [];
            foreach (var item in playerBody.ResponsiblePlayer)
            {
                bool validationResult = _entityValidationService.Validate(_createResponsiblePlayerValidator, item);
                if (!validationResult)
                {
                    _notificationContext.AddNotification(NotificationKeys.PlayerNotifications.ERROR_PLAYER_CREATED, string.Empty);
                    return null;
                }

                PlayerResponsible newPlayerResponsible = new();
                newPlayerResponsible.SetPlayerId(player.Id);
                newPlayerResponsible.SetRelationship(item.Relationship);
                newPlayerResponsible.SetIsPrimaryContact(item.IsPrimaryContact);
                newPlayerResponsible.SetMemberId(item.ResponsibleId);

                await _unitOfWork.PlayerResponsibleRepository.AddAsync(newPlayerResponsible);
                listPlayerResponsibles.Add(newPlayerResponsible);
            }
            return listPlayerResponsibles;
        }

        public async Task<Player?> UpdatePlayer(UpdatePlayerDTO playerToUpdate, Player player)
        {
            bool validationResult = _entityValidationService.Validate(_updatePlayerValidator, playerToUpdate);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.PlayerNotifications.ERROR_PLAYER_UPDATED, string.Empty);
                return null;
            }

            player.SetDateOfBirth(playerToUpdate.DateOfBirth);
            player.SetPosition(playerToUpdate.Position);
            player.SetNationality(playerToUpdate.Nationality);
            player.SetPreferredFoot(playerToUpdate.PreferredFoot);
            player.SetLastName(playerToUpdate.LastName);
            player.SetFirstName(playerToUpdate.FirstName);
            player.SetHeight(playerToUpdate.Height);
            player.SetWeight(playerToUpdate.Weight);
            player.SetPlayerCategoryId(playerToUpdate.PlayerCategoryId);

            _unitOfWork.PlayerRepository.Update(player);
            return player;
        }

        public async Task<List<PlayerResponsible>?> UpdatePlayerResponsible(UpdatePlayerDTO playerBody, Player player)
        {
            List<PlayerResponsible>? listPlayerResponsibles = [];
            foreach (var item in playerBody.ResponsiblePlayer)
            {
                PlayerResponsible playerResponsible = await _unitOfWork.PlayerResponsibleRepository.GetById(item.Id);
                bool validationResult = _entityValidationService.Validate(_updateResponsiblePlayerValidator, item);
                if (!validationResult && playerResponsible == null)
                {
                    _notificationContext.AddNotification(NotificationKeys.PlayerNotifications.ERROR_PLAYER_UPDATED, string.Empty);
                    return null;
                }

                playerResponsible.SetPlayerId((int)player.Id);
                playerResponsible.SetRelationship(item.Relationship);
                playerResponsible.SetIsPrimaryContact(item.IsPrimaryContact);
                playerResponsible.SetMemberId(item.ResponsibleId);

                _unitOfWork.PlayerResponsibleRepository.Update(playerResponsible);
                listPlayerResponsibles.Add(playerResponsible);
            }
            return listPlayerResponsibles;
        }

        #endregion

        #region PlayerTransfer
        public async Task<PlayerTransfer?> CreatePlayerTransfer(CreatePlayerTransferDTO playerTransferBody)
        {
            bool validationResult = _entityValidationService.Validate(_createPlayerTransferValidator, playerTransferBody);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.PlayerTransferNotifications.ERROR_PLAYERTRANSFER_CREATED, string.Empty);
                return null;
            }

            PlayerTransfer playerTransfer = new();
            playerTransfer.SetPlayerId(playerTransferBody.PlayerId);
            playerTransfer.SetToClub(playerTransferBody.ToClub);
            playerTransfer.SetFromClub(playerTransferBody.FromClub);
            playerTransfer.SetTransferDate(playerTransferBody.TransferDate);
            playerTransfer.SetTransferFee(playerTransferBody.TransferFee);

            return await _unitOfWork.PlayerTransferRepository.AddAsync(playerTransfer);
        }

        public async Task<PlayerTransfer?> UpdatePlayerTransfer(UpdatePlayerTransferDTO playerTransferToUpdate, PlayerTransfer playerTransfer)
        {
            bool validationResult = _entityValidationService.Validate(_updatePlayerTransferValidator, playerTransferToUpdate);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.UserNotifications.ERROR_USER_CREATED, string.Empty);
                return null;
            }

            playerTransfer.SetPlayerId(playerTransferToUpdate.PlayerId);
            playerTransfer.SetToClub(playerTransferToUpdate.ToClub);
            playerTransfer.SetFromClub(playerTransferToUpdate.FromClub);
            playerTransfer.SetTransferDate(playerTransferToUpdate.TransferDate);
            playerTransfer.SetTransferFee(playerTransferToUpdate.TransferFee);

            _unitOfWork.PlayerTransferRepository.Update(playerTransfer);
            return playerTransfer;
        }

        public async Task<PlayerTransfer?> DeletePlayerTransfer(long id)
        {
            PlayerTransfer? playerTransfer = await _unitOfWork.PlayerTransferRepository.GetByIdAsync(id);

            if (playerTransfer == null)
            {
                _notificationContext.AddNotification(NotificationKeys.PlayerTransferNotifications.PLAYERTRANSFER_DONT_EXITS, string.Empty);
                return null;
            }

            _unitOfWork.PlayerTransferRepository.Delete(playerTransfer);
            return playerTransfer;
        }
        #endregion

        #region PlayerContract
        public async Task<PlayerContract?> CreatePlayerContract(CreatePlayerContractDTO playerContractBody)
        {
            bool validationResult = _entityValidationService.Validate(_createPlayerContractValidator, playerContractBody);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.PlayerContractNotifications.ERROR_PLAYERCONTRACT_CREATED, string.Empty);
                return null;
            }

            PlayerContract playerContract = new();
            playerContract.SetContractType(playerContractBody.ContractType);
            playerContract.SetSalary(playerContractBody.Salary);
            playerContract.SetPlayerId(playerContractBody.PlayerId);
            playerContract.SetEndDate(playerContractBody.EndDate);
            playerContract.SetStartDate(playerContractBody.StartDate);

            return await _unitOfWork.PlayerContractRepository.AddAsync(playerContract);
        }

        public async Task<PlayerContract?> DeletePlayerContract(long id)
        {
            PlayerContract? playerContract = await _unitOfWork.PlayerContractRepository.GetByIdAsync(id);

            if (playerContract == null)
            {
                _notificationContext.AddNotification(NotificationKeys.PlayerContractNotifications.PLAYERCONTRACT_DONT_EXITS, string.Empty);
                return null;
            }

            _unitOfWork.PlayerContractRepository.Delete(playerContract);
            return playerContract;
        }

        public async Task<PlayerContract?> UpdatePlayerContract(UpdatePlayerContractDTO playerContractToUpdate, PlayerContract playerContract)
        {
            bool validationResult = _entityValidationService.Validate(_updatePlayerContractValidator, playerContractToUpdate);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.UserNotifications.ERROR_USER_CREATED, string.Empty);
                return null;
            }

            playerContract.SetContractType(playerContractToUpdate.ContractType);
            playerContract.SetSalary(playerContractToUpdate.Salary);
            playerContract.SetPlayerId(playerContractToUpdate.PlayerId);
            playerContract.SetEndDate(playerContractToUpdate.EndDate);
            playerContract.SetStartDate(playerContractToUpdate.StartDate);

            _unitOfWork.PlayerContractRepository.Update(playerContract);
            return playerContract;
        }
        #endregion

        #region PlayerPerformanceHistory
        public async Task<PlayerPerformanceHistory?> CreatePlayerPerformanceHistory(CreatePlayerPerformanceHistoryDTO playerPerformanceHistoryBody)
        {
            bool validationResult = _entityValidationService.Validate(_createPlayerPerformanceHistoryValidator, playerPerformanceHistoryBody);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.PlayerPerformanceHistoryNotifications.ERROR_PLAYERPERFORMANCEHISTORY_CREATED, string.Empty);
                return null;
            }

            PlayerPerformanceHistory playerPerformanceHistory = new();
            playerPerformanceHistory.SetAssists(playerPerformanceHistoryBody.Assists);
            playerPerformanceHistory.SetClubOpponent(playerPerformanceHistoryBody.ClubOpponent);
            playerPerformanceHistory.SetMinutesPlayed(playerPerformanceHistoryBody.MinutesPlayed);
            playerPerformanceHistory.SetSeason(playerPerformanceHistoryBody.Season);
            playerPerformanceHistory.SetPlayerId(playerPerformanceHistoryBody.PlayerId);
            playerPerformanceHistory.SetGoals(playerPerformanceHistoryBody.Goals);
            playerPerformanceHistory.SetYellowCards(playerPerformanceHistoryBody.YellowCards);
            playerPerformanceHistory.SetRedCards(playerPerformanceHistoryBody.RedCards);

            return await _unitOfWork.PlayerPerformanceHistoryRepository.AddAsync(playerPerformanceHistory);
        }

        public async Task<PlayerPerformanceHistory?> UpdatePlayerPerformanceHistory(UpdatePlayerPerformanceHistoryDTO playerPerformanceHistoryToUpdate, PlayerPerformanceHistory playerPerformanceHistory)
        {
            bool validationResult = _entityValidationService.Validate(_updatePlayerPerformanceHistoryValidator, playerPerformanceHistoryToUpdate);
            if (!validationResult)
            {
                _notificationContext.AddNotification(NotificationKeys.UserNotifications.ERROR_USER_CREATED, string.Empty);
                return null;
            }

            playerPerformanceHistory.SetAssists(playerPerformanceHistoryToUpdate.Assists);
            playerPerformanceHistory.SetClubOpponent(playerPerformanceHistoryToUpdate.ClubOpponent);
            playerPerformanceHistory.SetMinutesPlayed(playerPerformanceHistoryToUpdate.MinutesPlayed);
            playerPerformanceHistory.SetSeason(playerPerformanceHistoryToUpdate.Season);
            playerPerformanceHistory.SetPlayerId(playerPerformanceHistoryToUpdate.PlayerId);
            playerPerformanceHistory.SetGoals(playerPerformanceHistoryToUpdate.Goals);
            playerPerformanceHistory.SetYellowCards(playerPerformanceHistoryToUpdate.YellowCards);
            playerPerformanceHistory.SetRedCards(playerPerformanceHistoryToUpdate.RedCards);

            _unitOfWork.PlayerPerformanceHistoryRepository.Update(playerPerformanceHistory);
            return playerPerformanceHistory;
        }

        public async Task<PlayerPerformanceHistory?> DeletePlayerPerformanceHistory(long id)
        {
            PlayerPerformanceHistory? playerPerformanceHistory = await _unitOfWork.PlayerPerformanceHistoryRepository.GetById(id);

            if (playerPerformanceHistory == null)
            {
                _notificationContext.AddNotification(NotificationKeys.PlayerPerformanceHistoryNotifications.PLAYERPERFORMANCEHISTORY_DONT_EXITS, string.Empty);
                return null;
            }

            _unitOfWork.PlayerPerformanceHistoryRepository.Delete(playerPerformanceHistory);
            return playerPerformanceHistory;
        }
        #endregion
    }
}
