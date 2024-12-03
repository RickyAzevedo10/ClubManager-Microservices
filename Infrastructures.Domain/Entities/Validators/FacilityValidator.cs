using FluentValidation;
using Infrastructures.Domain.DTOs;

namespace Infrastructures.Domain.Entities.Validators
{
    public class FacilityValidator : AbstractValidator<CreateFacilityDTO>
    {
        public FacilityValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be null.")
                .NotNull().WithMessage("Name cannot be empty.");
            RuleFor(x => x.FacilityCategoryId)
                .NotEmpty().WithMessage("FacilityCategoryId cannot be empty.")
                .NotNull().WithMessage("FacilityCategoryId cannot be null.");
            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location cannot be empty.")
                .NotNull().WithMessage("Location cannot be null.");
        }
    }
}
