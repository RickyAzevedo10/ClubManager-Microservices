using Contracts;
using Financial.Domain.Entities;
using Financial.Domain.Interfaces.Identity;
using Financial.Domain.Interfaces.Repositories;
using MassTransit;

namespace Financial.Application.Consumers
{
    public class PlayerDeleteConsumer : IConsumer<DeletePlayer>
    {
        private readonly IPlayerService _playerService;
        private readonly IUnitOfWork _unitOfWork;

        public PlayerDeleteConsumer(IPlayerService playerService, IUnitOfWork unitOfWork)
        {
            _playerService = playerService;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<DeletePlayer> context)
        {
            Player? player = await _unitOfWork.PlayerRepository.GetById(context.Message.ExternalPlayerId);

            if (player != null)
            {
                //delete
                await _playerService.DeletePlayer(player);
                await _unitOfWork.CommitAsync();
            }

            return;
        }
    }
}
