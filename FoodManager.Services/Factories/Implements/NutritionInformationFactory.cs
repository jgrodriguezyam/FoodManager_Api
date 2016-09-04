using System;
using FoodManager.Infrastructure.Application;
using FoodManager.Infrastructure.Decimals;
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
                                            var netWeight = saucerConfiguration.NetWeight;

                                            nutritionInformation.Energy += (ingredient.Energy / ingredient.NetWeight) * netWeight;
                                            nutritionInformation.Protein += (ingredient.Protein / ingredient.NetWeight) * netWeight;
                                            nutritionInformation.Carbohydrate += (ingredient.Carbohydrate / ingredient.NetWeight) * netWeight;
                                            nutritionInformation.Sugar += (ingredient.Sugar / ingredient.NetWeight) * netWeight;
                                            nutritionInformation.Lipid += (ingredient.Lipid / ingredient.NetWeight) * netWeight;
                                            nutritionInformation.Sodium += (ingredient.Sodium / ingredient.NetWeight) * netWeight;
                                        });
            RoundDecimalAmout(nutritionInformation);
            return nutritionInformation;
        }

        private void RoundDecimalAmout(NutritionInformation nutritionInformation)
        {
            nutritionInformation.Energy = nutritionInformation.Energy.RoundDecimal();
            nutritionInformation.Protein = nutritionInformation.Protein.RoundDecimal();
            nutritionInformation.Carbohydrate = nutritionInformation.Carbohydrate.RoundDecimal();
            nutritionInformation.Sugar = nutritionInformation.Sugar.RoundDecimal();
            nutritionInformation.Lipid = nutritionInformation.Lipid.RoundDecimal();
            nutritionInformation.Sodium = nutritionInformation.Sodium.RoundDecimal();
        }
    }
}