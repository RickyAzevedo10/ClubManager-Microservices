using FluentValidation;
using TrainingCompetition.Domain.DTOs;

namespace TrainingCompetition.Domain.Entities.Validators
{
    public class TrainingAttendanceValidator : AbstractValidator<CreateTrainingAttendanceDTO>
    {
        public TrainingAttendanceValidator()
        {
            RuleFor(x => x.TrainingSessionId)
           .GreaterThan(0).WithMessage("The training session ID must be greater than zero.");

            RuleFor(x => x.PlayerId)
                .GreaterThan(0).WithMessage("The player ID must be greater than zero.");

            RuleFor(x => x.IsPresent)
                .NotNull().WithMessage("The presence status cannot be null.");

            When(x => !x.IsPresent, () =>
            {
                RuleFor(x => x.AbsenceReason)
                    .NotEmpty().WithMessage("The absence reason is required when the player is absent.")
                    .MaximumLength(500).WithMessage("The absence reason must be up to 500 characters long.");
            });
        }
    }
}
