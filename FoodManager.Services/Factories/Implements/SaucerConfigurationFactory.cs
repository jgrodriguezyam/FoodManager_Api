using FastMapper;
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

        public DTO.SaucerConfiguration Execute(SaucerConfiguration saucerConfiguration)
        {
            var saucerConfigurationDto = TypeAdapter.Adapt<DTO.SaucerConfiguration>(saucerConfiguration);
            var saucer = _saucerRepository.FindBy(saucerConfiguration.SaucerId);
            saucerConfigurationDto.Saucer = TypeAdapter.Adapt<DTO.Saucer>(saucer);
            var ingredient = _ingredientRepository.FindBy(saucerConfiguration.IngredientId);
            saucerConfigurationDto.Ingredient = TypeAdapter.Adapt<DTO.Ingredient>(ingredient);
            return saucerConfigurationDto;
        }
    }
}