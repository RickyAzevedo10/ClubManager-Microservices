using FluentValidation;
using Infrastructures.Domain.DTOs;

namespace Infrastructures.Domain.Entities.Validators
{
    public class FacilityReservationValidator : AbstractValidator<CreateFacilityReservationDTO>
    {
        public FacilityReservationValidator()
        {
            RuleFor(x => x.EventType)
                .NotEmpty().WithMessage("EventType cannot be null.")
                .NotNull().WithMessage("EventType cannot be empty.");
            RuleFor(x => x.FacilityId)
                .NotEmpty().WithMessage("FacilityId cannot be empty.")
                .NotNull().WithMessage("FacilityId cannot be null.");
            RuleFor(x => x.ReservedUserId)
                .NotEmpty().WithMessage("ReservedUserId cannot be empty.")
                .NotNull().WithMessage("ReservedUserId cannot be null.");
            RuleFor(x => x.StartTime)
                .NotEmpty()
                .WithMessage("Start time cannot be empty.")
                .GreaterThan(DateTime.Now)
                .WithMessage("Start time must be in the future.")
                .LessThan(x => x.EndTime)
                .WithMessage("Start time must be before the end time.");
            RuleFor(x => x.EndTime)
                .NotEmpty()
                .WithMessage("End time cannot be empty.")
                .GreaterThan(x => x.StartTime)
                .WithMessage("End time must be after the start time.");

        }
    }
}
