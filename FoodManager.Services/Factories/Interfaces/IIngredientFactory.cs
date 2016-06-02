using System.Collections.Generic;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IIngredientFactory
    {
        DTO.Ingredient Execute(Ingredient ingredient);
        List<Ingredient> FromCsv(string fileName);
    }
}