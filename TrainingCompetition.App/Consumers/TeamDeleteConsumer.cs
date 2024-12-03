using Contracts;
using MassTransit;
using TrainingCompetition.Domain.Entities;
using TrainingCompetition.Domain.Interfaces.Identity;
using TrainingCompetition.Domain.Interfaces.Repositories;

namespace TrainingCompetition.Application.Consumers
{
    public class TeamDeleteConsumer : IConsumer<DeleteTeam>
    {
        private readonly ITeamService _teamService;
        private readonly IUnitOfWork _unitOfWork;

        public TeamDeleteConsumer(ITeamService teamService, IUnitOfWork unitOfWork)
        {
            _teamService = teamService;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<DeleteTeam> context)
        {
            Team? team = await _unitOfWork.TeamRepository.GetById(context.Message.ExternalTeamId);

            if (team != null)
            {
                //delete
                await _teamService.DeleteTeam(team);
                await _unitOfWork.CommitAsync();
            }

            return;
        }
    }
}
