using FluentValidation;
using TrainingCompetition.Domain.DTOs;

namespace TrainingCompetition.Domain.Entities.Validators
{
    public class TrainingSessionValidator : AbstractValidator<CreateTrainingSessionDTO>
    {
        public TrainingSessionValidator()
        {
            RuleFor(x => x.TeamId)
                        .GreaterThan(0).WithMessage("The team ID must be greater than zero.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The name of the training session is required.")
                .Length(1, 100).WithMessage("The name of the training session must be between 1 and 100 characters.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("The date and time of the training session are required.")
                .GreaterThan(DateTime.Now).WithMessage("The date and time of the training session must be in the future.");

            RuleFor(x => x.Duration)
                .GreaterThan(0).WithMessage("The duration of the training session must be greater than zero.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("The location of the training session is required.")
                .Length(1, 200).WithMessage("The location of the training session must be between 1 and 200 characters.");

            RuleFor(x => x.Objectives)
                .NotEmpty().WithMessage("The objectives of the training session are required.");
        }
    }
}
