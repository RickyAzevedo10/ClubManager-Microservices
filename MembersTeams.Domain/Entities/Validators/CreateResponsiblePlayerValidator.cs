using FluentValidation;
using MembersTeams.Domain.DTOs;

namespace MembersTeams.Domain.Entities.Validators
{
    public class CreateResponsiblePlayerValidator : AbstractValidator<CreateResponsiblePlayerDTO>
    {
        public CreateResponsiblePlayerValidator()
        {
            RuleFor(x => x.ResponsibleId)
                .NotEmpty().WithMessage("ResponsibleId cannot be empty.")
                .NotNull().WithMessage("ResponsibleId cannot be null.");
            RuleFor(x => x.Relationship)
                .NotEmpty().WithMessage("Relationship cannot be empty.")
                .NotNull().WithMessage("Relationship cannot be null.");
        }
    }
}
