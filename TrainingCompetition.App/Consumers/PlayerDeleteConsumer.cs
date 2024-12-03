using Contracts;
using MassTransit;
using TrainingCompetition.Domain.Entities;
using TrainingCompetition.Domain.Interfaces.Identity;
using TrainingCompetition.Domain.Interfaces.Repositories;

namespace TrainingCompetition.Application.Consumers
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
