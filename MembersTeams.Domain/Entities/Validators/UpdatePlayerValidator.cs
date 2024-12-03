using FluentValidation;
using MembersTeams.Domain.DTOs;

namespace MembersTeams.Domain.Entities.Validators
{
    public class UpdatePlayerValidator : AbstractValidator<UpdatePlayerDTO>
    {
        public UpdatePlayerValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
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
