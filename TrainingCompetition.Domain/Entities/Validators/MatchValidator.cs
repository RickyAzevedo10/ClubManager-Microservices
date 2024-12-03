using FluentValidation;
using TrainingCompetition.Domain.DTOs;

namespace TrainingCompetition.Domain.Entities.Validators
{
    public class MatchValidator : AbstractValidator<CreateMatchDTO>
    {
        public MatchValidator()
        {
            RuleFor(x => x.Date)
            .NotEmpty().WithMessage("The date and time of the match are required.")
            .GreaterThanOrEqualTo(DateTime.Now).WithMessage("The date and time of the match must be in the future.");

            RuleFor(x => x.Opponent)
                .NotEmpty().WithMessage("The opponent's name is required.")
                .Length(1, 100).WithMessage("The opponent's name must be between 1 and 100 characters.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("The match location is required.")
                .Length(1, 200).WithMessage("The match location must be between 1 and 200 characters.");

            RuleFor(x => x.CompetitionType)
                .NotEmpty().WithMessage("The competition type is required.")
                .Length(1, 50).WithMessage("The competition type must be between 1 and 50 characters.");

            RuleFor(x => x.TeamId)
                .GreaterThan(0).WithMessage("The team ID must be greater than zero.");

            RuleFor(x => x.IsCanceled)
                .NotNull().WithMessage("The canceled field cannot be null.");
        }
    }
}
