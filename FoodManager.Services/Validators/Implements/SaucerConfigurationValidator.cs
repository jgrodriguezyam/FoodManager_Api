using FluentValidation;
using FluentValidation.Results;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Objects;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Validators.Implements
{
    public class SaucerConfigurationValidator : BaseValidator<SaucerConfiguration>, ISaucerConfigurationValidator
    {
        private readonly ISaucerRepository _saucerRepository;
        private readonly IIngredientRepository _ingredientRepository;

        public SaucerConfigurationValidator(ISaucerRepository saucerRepository, IIngredientRepository ingredientRepository)
        {
            _saucerRepository = saucerRepository;
            _ingredientRepository = ingredientRepository;

            RuleSet("Base", () =>
            {
                RuleFor(saucerConfiguration => saucerConfiguration.SaucerId).Must(saucerId => saucerId.IsNotZero()).WithMessage("Tienes que elegir un platillo");
                RuleFor(saucerConfiguration => saucerConfiguration.IngredientId).Must(ingredientId => ingredientId.IsNotZero()).WithMessage("Tienes que elegir un ingrediente");
                RuleFor(saucerConfiguration => saucerConfiguration.Amount).Must(amount => amount.IsNotZero()).WithMessage("Tienes que elegir una cantidad");
                Custom(ReferencesValidate);
            });
        }

        public ValidationFailure ReferencesValidate(SaucerConfiguration saucerConfiguration, ValidationContext<SaucerConfiguration> context)
        {
            var saucer = _saucerRepository.FindBy(saucerConfiguration.SaucerId);
            if (saucer.IsNull() || saucer.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("SaucerConfiguration", "El platillo esta desactivado o no existe");

            var ingredient = _ingredientRepository.FindBy(saucerConfiguration.IngredientId);
            if (ingredient.IsNull() || ingredient.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("SaucerConfiguration", "El ingrediente esta desactivado o no existe");

            return null;
        }
    }
}