using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using FoodManager.Infrastructure.Enums;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Objects;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Model.Enums;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Validators.Implements
{
    public class SaucerValidator : BaseValidator<Saucer>, ISaucerValidator
    {
        public SaucerValidator()
        {
            RuleSet("Base", () =>
            {
                RuleFor(saucer => saucer.Name).NotNull().NotEmpty();
                RuleFor(saucer => saucer.Type).Must(saucerType => saucerType.IsNotZero()).WithMessage("Tienes que elegir un tipo");
                Custom(ReferencesValidate);
            });
        }

        public ValidationFailure ReferencesValidate(Saucer saucer, ValidationContext<Saucer> context)
        {
            var saucerType = new SaucerType().ConvertToCollection().FirstOrDefault(saucerTp => saucerTp.Value == saucer.Type);
            if (saucerType.IsNull())
                return new ValidationFailure("Saucer", "El tipo de platillo no existe");

            return null;
        }
    }
}