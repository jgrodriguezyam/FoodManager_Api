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
                                            var portion = saucerConfiguration.Portion;

                                            nutritionInformation.Energy += (ingredient.Energy * portion);
                                            nutritionInformation.Protein += (ingredient.Protein * portion);
                                            nutritionInformation.Carbohydrate += (ingredient.Carbohydrate * portion);
                                            nutritionInformation.Sugar += (ingredient.Sugar * portion);
                                            nutritionInformation.Lipid += (ingredient.Lipid * portion);
                                            nutritionInformation.Sodium += (ingredient.Sodium * portion);
                                        });
            return nutritionInformation;
        }
    }
}