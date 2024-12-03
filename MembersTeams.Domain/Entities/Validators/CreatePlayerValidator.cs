using FluentValidation;
using MembersTeams.Domain.DTOs;

namespace MembersTeams.Domain.Entities.Validators
{
    public class CreatePlayerValidator : AbstractValidator<CreatePlayerDTO>
    {
        public CreatePlayerValidator()
        {
            RuleFor(x => x.PlayerCategoryId)
                .NotEmpty().WithMessage("PlayerCategoryId cannot be null.")
                .NotNull().WithMessage("PlayerCategoryId cannot be empty.");
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName cannot be empty.")
                .NotNull().WithMessage("FirstName cannot be null.");
            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("Position cannot be empty.")
                .NotNull().WithMessage("Position cannot be null.");
        }
    }
}
