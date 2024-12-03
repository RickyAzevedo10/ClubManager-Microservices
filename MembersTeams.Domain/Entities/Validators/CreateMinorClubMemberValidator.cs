using FluentValidation;
using MembersTeams.Domain.DTOs;

namespace MembersTeams.Domain.Entities.Validators
{
    public class CreateMinorClubMemberValidator : AbstractValidator<CreateMinorClubMemberDTO>
    {
        public CreateMinorClubMemberValidator()
        {
            RuleFor(x => x.MemberId).NotEmpty();
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Name cannot be null.")
                .NotNull().WithMessage("Name cannot be empty.");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address cannot be empty.")
                .NotNull().WithMessage("Address cannot be null.");
        }
    }
}
