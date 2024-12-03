using FluentValidation;
using MembersTeams.Domain.DTOs;

namespace MembersTeams.Domain.Entities.Validators
{
    public class UpdatePlayerPerformanceHistoryValidator : AbstractValidator<UpdatePlayerPerformanceHistoryDTO>
    {
        public UpdatePlayerPerformanceHistoryValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.PlayerId).NotNull().NotEmpty();
            RuleFor(x => x.ClubOpponent)
                .NotEmpty().WithMessage("ClubOpponent cannot be null.")
                .NotNull().WithMessage("ClubOpponent cannot be empty.");
            RuleFor(x => x.Season)
                .NotEmpty().WithMessage("Season cannot be empty.")
                .NotNull().WithMessage("Season cannot be null.");
        }
    }
}
