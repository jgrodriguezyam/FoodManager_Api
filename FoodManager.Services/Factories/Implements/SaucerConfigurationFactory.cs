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
            return AppendProperties(new[] { saucerConfiguration }).FirstOrDefault();
        }

        public IEnumerable<SaucerConfigurationResponse> Execute(IEnumerable<SaucerConfiguration> saucerConfigurations)
        {
            return AppendProperties(saucerConfigurations);
        }

        private IEnumerable<SaucerConfigurationResponse> AppendProperties(IEnumerable<SaucerConfiguration> saucerConfigurations)
        {
            var saucerConfigurationsResponse = TypeAdapter.Adapt<List<SaucerConfigurationResponse>>(saucerConfigurations);            
            var saucers = _saucerRepository.FindBy(saucer => saucer.IsActive);
            var ingredients = _ingredientRepository.FindBy(ingredient => ingredient.IsActive);

            saucerConfigurationsResponse.ForEach(saucerConfigurationResponse =>
            {
                var saucerConfiguration = saucerConfigurations.First(saucerConfigurationModel => saucerConfigurationModel.Id == saucerConfigurationResponse.Id);
                var saucer = saucers.First(saucerModel => saucerModel.Id == saucerConfiguration.SaucerId);
                saucerConfigurationResponse.Saucer = TypeAdapter.Adapt<SaucerResponse>(saucer);
                var ingredient = ingredients.First(ingredientModel => ingredientModel.Id == saucerConfiguration.IngredientId);
                saucerConfigurationResponse.Ingredient = TypeAdapter.Adapt<IngredientResponse>(ingredient);                
            });

            return saucerConfigurationsResponse;
        }
    }
}