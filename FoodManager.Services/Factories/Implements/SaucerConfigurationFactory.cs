using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Ingredients;
using FoodManager.DTO.Message.SaucerConfigurations;
using FoodManager.DTO.Message.Saucers;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class SaucerConfigurationFactory : ISaucerConfigurationFactory
    {
        private readonly ISaucerRepository _saucerRepository;
        private readonly IIngredientRepository _ingredientRepository;

        public SaucerConfigurationFactory(ISaucerRepository saucerRepository, IIngredientRepository ingredientRepository)
        {
            _saucerRepository = saucerRepository;
            _ingredientRepository = ingredientRepository;
        }

        public SaucerConfigurationResponse Execute(SaucerConfiguration saucerConfiguration)
        {
            var saucerConfigurationResponse = TypeAdapter.Adapt<SaucerConfigurationResponse>(saucerConfiguration);
            var saucer = _saucerRepository.FindBy(saucerConfiguration.SaucerId);
            saucerConfigurationResponse.Saucer = TypeAdapter.Adapt<SaucerResponse>(saucer);
            var ingredient = _ingredientRepository.FindBy(saucerConfiguration.IngredientId);
            saucerConfigurationResponse.Ingredient = TypeAdapter.Adapt<IngredientResponse>(ingredient);
            return saucerConfigurationResponse;
        }

        public IEnumerable<SaucerConfigurationResponse> Execute(IEnumerable<SaucerConfiguration> saucerConfigurations)
        {
            return saucerConfigurations.Select(Execute);
        }
    }
}