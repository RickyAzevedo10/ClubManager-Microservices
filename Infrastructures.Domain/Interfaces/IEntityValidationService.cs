using FluentValidation;

namespace Infrastructures.Domain.Interfaces
{
    public interface IEntityValidationService
    {
        bool Validate<T>(IValidator<T> validator, T entity);
    }
}
