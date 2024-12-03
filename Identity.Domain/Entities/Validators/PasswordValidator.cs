using FluentValidation;
using Identity.Domain.DTOs;
using Identity.Domain.Utils;

namespace Identity.Domain.Entities.Validators
{
    public class PasswordValidator : AbstractValidator<ResetPasswordDTO>
    {
        public PasswordValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token cannot be null.")
                .NotNull().WithMessage("Token cannot be empty.");
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("The password must not be empty.")
                .NotNull().WithMessage("The password must not be null.")
                .MinimumLength(8).WithMessage("The password must have at least 8 characters.")
                .Must(PasswordValidation.ContainsUpperCaseLetter).WithMessage("The password must have at least 1 uppercase letter.")
                .Must(PasswordValidation.ContainsDigit).WithMessage("The password must have at least 1 number.");
            RuleFor(x => x.RepeatNewPassword)
                .Matches(x => x.NewPassword);
        }
    }
}
