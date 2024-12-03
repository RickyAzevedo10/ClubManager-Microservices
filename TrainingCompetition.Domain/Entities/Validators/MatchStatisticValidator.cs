using FluentValidation;
using TrainingCompetition.Domain.DTOs;

namespace TrainingCompetition.Domain.Entities.Validators
{
    public class MatchStatisticValidator : AbstractValidator<CreateMatchStatisticDTO>
    {
        public MatchStatisticValidator()
        {
            RuleFor(x => x.MatchId)
            .GreaterThan(0).WithMessage("The match ID must be greater than zero.");

            RuleFor(x => x.PlayerId)
                .GreaterThan(0).WithMessage("The player ID must be greater than zero.");

            RuleFor(x => x.Goals)
                .GreaterThanOrEqualTo(0).WithMessage("The number of goals must be zero or greater.");

            RuleFor(x => x.Assists)
                .GreaterThanOrEqualTo(0).WithMessage("The number of assists must be zero or greater.");

            RuleFor(x => x.YellowCards)
                .GreaterThanOrEqualTo(0).WithMessage("The number of yellow cards must be zero or greater.");

            RuleFor(x => x.RedCards)
                .GreaterThanOrEqualTo(0).WithMessage("The number of red cards must be zero or greater.");

            RuleFor(x => x.MinutesPlayed)
                .GreaterThanOrEqualTo(0).WithMessage("The number of minutes played must be zero or greater.");

            RuleFor(x => x.Substitutions)
                .GreaterThanOrEqualTo(0).WithMessage("The number of substitutions must be zero or greater.");
        }
    }
}
