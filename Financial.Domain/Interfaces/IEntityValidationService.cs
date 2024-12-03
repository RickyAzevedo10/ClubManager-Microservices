using FluentValidation;

namespace Financial.Domain.Interfaces
{
    public interface IEntityValidationService
    {
        bool Validate<T>(IValidator<T> validator, T entity);
    }
}
