using FluentValidation;
using Identity.Domain.DTOs;

namespace Identity.Domain.Entities.Validators
{
    public class UpdateUserPermissionsValidator : AbstractValidator<UpdateUserPermissionsDTO>
    {
        public UpdateUserPermissionsValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId cannot be null.")
                .NotNull().WithMessage("UserId cannot be empty.");
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
