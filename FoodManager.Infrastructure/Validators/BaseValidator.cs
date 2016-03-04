using System.Linq;
using FluentValidation;
using FoodManager.Infrastructure.Exceptions;

namespace FoodManager.Infrastructure.Validators
{
    public class BaseValidator<T> : AbstractValidator<T>, IValidator<T>
    {
        public void ValidateAndThrowException(T request, string ruleSet)
        {
            try
            {
                var errors = Validate(request).Errors.Concat(this.Validate(request, ruleSet: ruleSet).Errors).ToList();
                if (errors.Any())
                    throw new ValidationException(errors);
            }
            catch (ValidationException exception)
            {
                var message = string.Join(" ", exception.Errors.Select(error => error.ErrorMessage));
                throw new InvalidRequestException(message, exception);
            }
        }

        public void ValidateAndThrowException(T request)
        {
            try
            {
                var errors = Validate(request).Errors.ToList();
                if (errors.Any())
                    throw new ValidationException(errors);
            }
            catch (ValidationException exception)
            {
                var message = string.Join(" ", exception.Errors.Select(error => error.ErrorMessage));
                throw new InvalidRequestException(message, exception);
            }
        }
    }
}