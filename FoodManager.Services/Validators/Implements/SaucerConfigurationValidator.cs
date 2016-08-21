using FluentValidation;
using FluentValidation.Results;
using FoodManager.Infrastructure.Collections;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Decimals;
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
        private readonly ISaucerConfigurationRepository _saucerConfigurationRepository;

        public SaucerConfigurationValidator(ISaucerRepository saucerRepository, IIngredientRepository ingredientRepository, ISaucerConfigurationRepository saucerConfigurationRepository)
        {
            _saucerRepository = saucerRepository;
            _ingredientRepository = ingredientRepository;
            _saucerConfigurationRepository = saucerConfigurationRepository;

            RuleSet("Base", () =>
            {
                RuleFor(saucerConfiguration => saucerConfiguration.SaucerId).Must(saucerId => saucerId.IsNotZero()).WithMessage("Tienes que elegir un platillo");
                RuleFor(saucerConfiguration => saucerConfiguration.IngredientId).Must(ingredientId => ingredientId.IsNotZero()).WithMessage("Tienes que elegir un ingrediente");
                RuleFor(saucerConfiguration => saucerConfiguration.NetWeight).Must(netWeight => netWeight.IsNotZero()).WithMessage("Tienes que elegir un peso neto");
                Custom(ReferencesValidate);
            });

            RuleSet("Create", () =>
            {
                Custom(CreateConfigurationValidate);
            });

            RuleSet("Update", () =>
            {
                Custom(UpdateConfigurationValidate);
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

        public ValidationFailure CreateConfigurationValidate(SaucerConfiguration saucerConfiguration, ValidationContext<SaucerConfiguration> context)
        {
            var saucerConfigurationsRetrieved = _saucerConfigurationRepository.FindBy(saucerConfig => saucerConfig.SaucerId == saucerConfiguration.SaucerId && saucerConfig.IngredientId == saucerConfiguration.IngredientId && saucerConfig.IsActive);
            if (saucerConfigurationsRetrieved.IsNotEmpty())
                return new ValidationFailure("SaucerConfiguration", "Ya existe configuracion");

            return null;
        }

        public ValidationFailure UpdateConfigurationValidate(SaucerConfiguration saucerConfiguration, ValidationContext<SaucerConfiguration> context)
        {
            var saucerConfigurationsRetrieved = _saucerConfigurationRepository.FindBy(saucerConfig => saucerConfig.SaucerId == saucerConfiguration.SaucerId && saucerConfig.IngredientId == saucerConfiguration.IngredientId && saucerConfig.Id != saucerConfiguration.Id && saucerConfig.IsActive);
            if (saucerConfigurationsRetrieved.IsNotEmpty())
                return new ValidationFailure("SaucerConfiguration", "Ya existe configuracion");

            return null;
        }
    }
}