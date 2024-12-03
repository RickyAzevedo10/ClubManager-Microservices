using Financial.Domain.DTOs;
using FluentValidation;

namespace Financial.Domain.Entities.Validators
{
    public class ExpenseValidator : AbstractValidator<ExpenseDTO>
    {
        public ExpenseValidator()
        {
            RuleFor(x => x.ExpenseDate)
                .NotEmpty()
                .WithMessage("The 'ExpenseDate' is required.");

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("The 'Amount' must be greater than zero.");

            RuleFor(x => x.Destination)
                .NotEmpty()
                .WithMessage("The 'Destination' is required.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .WithMessage("The 'CategoryId' is required and must be greater than zero.");

            RuleFor(x => x.EntityId)
                .GreaterThan(0)
                .WithMessage("The 'EntityId' is required and must be greater than zero.");

            RuleFor(x => x.ResponsibleUserId)
                .GreaterThan(0)
                .WithMessage("The 'ResponsibleUserId' is required and must be greater than zero.");
        }
    }
}
