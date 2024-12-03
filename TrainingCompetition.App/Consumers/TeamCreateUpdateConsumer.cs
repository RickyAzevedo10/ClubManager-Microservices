using Contracts;
using MassTransit;
using TrainingCompetition.Domain.Entities;
using TrainingCompetition.Domain.Interfaces.Identity;
using TrainingCompetition.Domain.Interfaces.Repositories;

namespace TrainingCompetition.Application.Consumers
{
    public class TeamCreateUpdateConsumer : IConsumer<CreateUpdateTeam>
    {
        private readonly ITeamService _teamService;
        private readonly IUnitOfWork _unitOfWork;

        public TeamCreateUpdateConsumer(ITeamService teamService, IUnitOfWork unitOfWork)
        {
            _teamService = teamService;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<CreateUpdateTeam> context)
        {
            Team? team = await _unitOfWork.TeamRepository.GetById(context.Message.ExternalTeamId);

            if (team == null)
            {
                //create
                await _teamService.CreateTeam(context.Message);
            }
            else
            {
                //update
                await _teamService.UpdateTeam(context.Message, team);
            }
            await _unitOfWork.CommitAsync();
            return;
        }
    }
}
