using System.Collections.Generic;
using FoodManager.DTO.Message.IngredientGroups;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IIngredientGroupFactory
    {
        IngredientGroupResponse Execute(IngredientGroup ingredientGroup);
        IEnumerable<IngredientGroupResponse> Execute(IEnumerable<IngredientGroup> ingredientGroups);
        List<IngredientGroup> FromCsv(string fileName);
    }
}