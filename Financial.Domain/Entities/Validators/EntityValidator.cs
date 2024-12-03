using Financial.Domain.DTOs;
using FluentValidation;

namespace Financial.Domain.Entities.Validators
{
    public class EntityValidator : AbstractValidator<CreateEntityDTO>
    {
        public EntityValidator()
        {
            When(x => x.Internal, () =>
            {
                RuleFor(x => x.ClubMemberId).NotNull().When(x => !x.PlayerId.HasValue)
                    .WithMessage("When 'Internal' is true, either 'ClubMemberId' or 'PlayerId' must be provided.");

                RuleFor(x => x.PlayerId).NotNull().When(x => !x.ClubMemberId.HasValue)
                    .WithMessage("When 'Internal' is true, either 'ClubMemberId' or 'PlayerId' must be provided.");

                RuleFor(x => x.ClubMemberId).Null().When(x => x.PlayerId.HasValue)
                    .WithMessage("Only one of 'ClubMemberId' or 'PlayerId' should be provided.");

                RuleFor(x => x.PlayerId).Null().When(x => x.ClubMemberId.HasValue)
                    .WithMessage("Only one of 'ClubMemberId' or 'PlayerId' should be provided.");
            });

            RuleFor(x => x.EntityType)
                .NotEmpty()
                .WithMessage("The 'EntityType' is required.");

            RuleFor(x => x.EntityName)
                .NotEmpty()
                .WithMessage("The 'EntityName' is required.");
        }
    }
}
