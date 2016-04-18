using FoodManager.Infrastructure.Application;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;
using ServiceStack.Common.Extensions;

namespace FoodManager.Services.Factories.Implements
{
    public class NutritionInformationFactory : INutritionInformationFactory
    {
        private readonly ISaucerConfigurationRepository _saucerConfigurationRepository;
        private readonly IIngredientRepository _ingredientRepository;

        public NutritionInformationFactory(ISaucerConfigurationRepository saucerConfigurationRepository, IIngredientRepository ingredientRepository)
        {
            _saucerConfigurationRepository = saucerConfigurationRepository;
            _ingredientRepository = ingredientRepository;
        }

        public NutritionInformation FindBySaucer(int saucerId)
        {
            var nutritionInformation = new NutritionInformation();
            var saucerConfigurations = _saucerConfigurationRepository.FindBy(saucerConfiguration => saucerConfiguration.SaucerId == saucerId && saucerConfiguration.Status);
            saucerConfigurations.ForEach(saucerConfiguration =>
                                        {
                                            var ingredient = _ingredientRepository.FindBy(saucerConfiguration.IngredientId);
                                            var amount = saucerConfiguration.Amount;

                                            nutritionInformation.Energy += (ingredient.Energy * amount);
                                            nutritionInformation.Protein += (ingredient.Protein * amount);
                                            nutritionInformation.Carbohydrate += (ingredient.Carbohydrate * amount);
                                            nutritionInformation.Sugar += (ingredient.Sugar * amount);
                                            nutritionInformation.Lipid += (ingredient.Lipid * amount);
                                            nutritionInformation.Sodium += (ingredient.Sodium * amount);
                                        });
            return nutritionInformation;
        }
    }
}