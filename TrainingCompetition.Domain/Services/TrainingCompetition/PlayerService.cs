using Contracts;
using TrainingCompetition.Domain.Entities;
using TrainingCompetition.Domain.Interfaces.Identity;
using TrainingCompetition.Domain.Interfaces.Repositories;

namespace TrainingCompetition.Domain.Services.Identity
{
    public class PlayerService : IPlayerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlayerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Player
        public async Task<Player?> DeletePlayer(Player? player)
        {
            _unitOfWork.PlayerRepository.Delete(player);
            return player;
        }

        public async Task<Player?> CreatePlayer(CreateUpdatePlayer playerBody)
        {
            Player player = new();
            player.SetLastName(playerBody.LastName);
            player.SetFirstName(playerBody.FirstName);

            return await _unitOfWork.PlayerRepository.AddAsync(player);
        }

        public async Task<Player?> UpdatePlayer(CreateUpdatePlayer playerToUpdate, Player player)
        {
            player.SetLastName(playerToUpdate.LastName);
            player.SetFirstName(playerToUpdate.FirstName);

            _unitOfWork.PlayerRepository.Update(player);
            return player;
        }

        #endregion


    }
}
