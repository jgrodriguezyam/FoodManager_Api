using FastMapper;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class IngredientFactory : IIngredientFactory
    {
        private readonly IIngredientGroupRepository _ingredientGroupRepository;

        public IngredientFactory(IIngredientGroupRepository ingredientGroupRepository)
        {
            _ingredientGroupRepository = ingredientGroupRepository;
        }

        public DTO.Ingredient Execute(Ingredient ingredient)
        {
            var ingredientDto = TypeAdapter.Adapt<DTO.Ingredient>(ingredient);
            var ingredientGroup = _ingredientGroupRepository.FindBy(ingredient.IngredientGroupId);
            ingredientDto.IngredientGroup = TypeAdapter.Adapt<DTO.IngredientGroup>(ingredientGroup);
            return ingredientDto;
        }
    }
}