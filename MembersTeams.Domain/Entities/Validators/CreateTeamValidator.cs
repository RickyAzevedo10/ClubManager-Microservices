using FluentValidation;
using MembersTeams.Domain.DTOs;

namespace MembersTeams.Domain.Entities.Validators
{
    public class CreateTeamValidator : AbstractValidator<CreateTeamDTO>
    {
        public CreateTeamValidator()
        {
            RuleFor(x => x.TeamCategoryId)
                .NotEmpty().WithMessage("TeamCategoryId cannot be null.")
                .NotNull().WithMessage("TeamCategoryId cannot be empty.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty.")
                .NotNull().WithMessage("Name cannot be null.");
            RuleFor(x => x.Male)
                .Must(x => x == true || x == false).WithMessage("Male must be a boolean value.");
            RuleFor(x => x.Female)
                .Must(x => x == true || x == false).WithMessage("Female must be a boolean value.");
            RuleFor(x => x.ClubId)
                .NotEmpty().WithMessage("ClubId cannot be empty.")
                .NotNull().WithMessage("ClubId cannot be null.");
        }
    }
}
