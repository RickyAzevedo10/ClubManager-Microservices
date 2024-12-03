using Contracts;
using Financial.Domain.Entities;
using Financial.Domain.Interfaces.Identity;
using Financial.Domain.Interfaces.Repositories;

namespace Financial.Domain.Services.Identity
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
