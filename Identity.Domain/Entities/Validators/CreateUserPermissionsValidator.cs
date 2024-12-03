using FluentValidation;
using Identity.Domain.DTOs;

namespace Identity.Domain.Entities.Validators
{
    public class CreateUserPermissionsValidator : AbstractValidator<CreateUserPermissionsDTO>
    {
        public CreateUserPermissionsValidator()
        {
            RuleFor(x => x.Consult)
                .Must(x => x == true || x == false).WithMessage("Consult must be a boolean value.");

            RuleFor(x => x.Edit)
                .Must(x => x == true || x == false).WithMessage("Edit must be a boolean value.");

            RuleFor(x => x.Delete)
                .Must(x => x == true || x == false).WithMessage("Delete must be a boolean value.");

            RuleFor(x => x.Create)
                .Must(x => x == true || x == false).WithMessage("Create must be a boolean value.");

        }
    }
}
