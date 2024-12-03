using Identity.Domain.Interfaces;
using FluentValidation;

namespace Identity.Domain.Services
{
    public class EntityValidationService : IEntityValidationService
    {
        private readonly IModelErrorsContext _modelErrorsContext;

        public EntityValidationService(IModelErrorsContext modelErrorContext)
        {
            _modelErrorsContext = modelErrorContext;
        }

        public bool Validate<T>(IValidator<T> validator, T entity)
        {
            var validationResult = validator.Validate(entity);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    _modelErrorsContext.AddModelError(typeof(T).Name, error.PropertyName, error.ErrorMessage);
                }
                return false;
            }
            return true;
        }
    }
}
