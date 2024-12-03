using Contracts;
using Financial.Domain.Entities;
using Financial.Domain.Interfaces.Identity;
using Financial.Domain.Interfaces.Repositories;
using MassTransit;

namespace Financial.Application.Consumers
{
    public class PlayerCreateUpdateConsumer : IConsumer<CreateUpdatePlayer>
    {
        private readonly IPlayerService _playerService;
        private readonly IUnitOfWork _unitOfWork;

        public PlayerCreateUpdateConsumer(IPlayerService playerService, IUnitOfWork unitOfWork)
        {
            _playerService = playerService;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<CreateUpdatePlayer> context)
        {
            Player? player = await _unitOfWork.PlayerRepository.GetById(context.Message.ExternalPlayerId);

            if (player == null)
            {
                //create
                await _playerService.CreatePlayer(context.Message);
            }
            else
            {
                //update
                await _playerService.UpdatePlayer(context.Message, player);
            }
            await _unitOfWork.CommitAsync();
            return;
        }
    }
}
