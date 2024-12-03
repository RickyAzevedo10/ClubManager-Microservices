using FluentValidation;
using Identity.Domain.DTOs;

namespace Identity.Domain.Entities.Validators
{
    public class InstitutionValidator : AbstractValidator<CreateInstitutionDTO>
    {
        public InstitutionValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be null.")
                .NotNull().WithMessage("Name cannot be empty.");
            RuleFor(user => user.Address)
                .NotEmpty().WithMessage("Address cannot be empty.")
                .NotNull().WithMessage("Address cannot be null.");
            RuleFor(x => x.Abbreviation)
                .MaximumLength(5).WithMessage("A abreviação não pode ter mais que 5 caracteres.");
            RuleFor(x => x.FoundationDate)
                .LessThanOrEqualTo(DateTime.Today).WithMessage("A data de fundação não pode ser futura.");
            RuleFor(x => x.StadiumCapacity)
                .GreaterThan(0).WithMessage("A capacidade do estádio deve ser maior que 0.");
            RuleFor(x => x.StadiumInauguration)
                .LessThanOrEqualTo(DateTime.Today).WithMessage("A data de inauguração do estádio não pode ser futura.");
        }
    }
}
