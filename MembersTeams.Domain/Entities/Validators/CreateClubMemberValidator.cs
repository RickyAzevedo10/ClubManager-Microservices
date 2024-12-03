using FluentValidation;
using MembersTeams.Domain.DTOs;

namespace MembersTeams.Domain.Entities.Validators
{
    public class CreateClubMemberValidator : AbstractValidator<CreateClubMemberDTO>
    {
        public CreateClubMemberValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Name cannot be null.")
                .NotNull().WithMessage("Name cannot be empty.");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address cannot be empty.")
                .NotNull().WithMessage("Address cannot be null.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-mail cannot be null.")
                .NotNull().WithMessage("E-mail cannot be empty.")
                .EmailAddress().WithMessage("E-mail must be valid.");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.");
        }
    }
}
