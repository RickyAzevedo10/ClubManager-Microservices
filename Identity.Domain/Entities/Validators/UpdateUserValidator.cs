using FluentValidation;
using Identity.Domain.DTOs;
using Identity.Domain.Utils;

namespace Identity.Domain.Entities.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-mail cannot be null.")
                .NotNull().WithMessage("E-mail cannot be empty.")
                .EmailAddress().WithMessage("E-mail must be valid.");
            RuleFor(user => user.Username)
                .NotEmpty().WithMessage("Username cannot be empty.")
                .NotNull().WithMessage("Username cannot be null.");
            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("The password must not be empty.")
                .NotNull().WithMessage("The password must not be null.")
                .MinimumLength(8).WithMessage("The password must have at least 8 characters.")
                .Must(PasswordValidation.ContainsUpperCaseLetter).WithMessage("The password must have at least 1 uppercase letter.")
                .Must(PasswordValidation.ContainsDigit).WithMessage("The password must have at least 1 number.");
            RuleFor(user => user.RoleId)
                .NotEmpty().WithMessage("RoleId cannot be empty.")
                .NotNull().WithMessage("RoleId cannot be null.")
                .InclusiveBetween(1, 7).WithMessage("RoleId must be between 1 and 7.");

        }
    }
}
