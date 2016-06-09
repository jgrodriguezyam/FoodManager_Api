using System.Collections.Generic;
using FoodManager.DTO.Message.Ingredients;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IIngredientFactory
    {
        IngredientResponse Execute(Ingredient ingredient);
        IEnumerable<IngredientResponse> Execute(IEnumerable<Ingredient> ingredients);
        List<Ingredient> FromCsv(string fileName);
    }
}