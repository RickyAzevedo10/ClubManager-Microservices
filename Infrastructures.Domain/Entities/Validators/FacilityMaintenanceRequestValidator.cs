using FluentValidation;
using Infrastructures.Domain.DTOs;

namespace Infrastructures.Domain.Entities.Validators
{
    public class FacilityMaintenanceRequestValidator : AbstractValidator<CreateMaintenanceRequestDTO>
    {
        public FacilityMaintenanceRequestValidator()
        {
            RuleFor(x => x.FacilityId)
                            .GreaterThan(0)
                            .WithMessage("The Facility ID must be greater than zero.");

            RuleFor(x => x.MaintenanceType)
                .NotEmpty()
                .WithMessage("Maintenance type cannot be empty.");

            RuleFor(x => x.Priority)
                .NotEmpty()
                .WithMessage("Priority cannot be empty.")
                .Matches("^(High|Medium|Low)$")
                .WithMessage("Priority must be 'High', 'Medium', or 'Low'.");

            RuleFor(x => x.RequestDate)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Request date must be today or a future date.");

            RuleFor(x => x.RequestedUserId)
                .GreaterThan(0)
                .WithMessage("The ID of the user must be greater than zero.");

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Status cannot be empty.")
                .Matches("^(Pending|In Progress|Completed)$")
                .WithMessage("Status must be 'Pending', 'In Progress', or 'Completed'.");
        }
    }
}
