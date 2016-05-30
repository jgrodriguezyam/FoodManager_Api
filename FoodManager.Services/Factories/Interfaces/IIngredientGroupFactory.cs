using System.Collections.Generic;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IIngredientGroupFactory
    {
        List<IngredientGroup> FromCsv(string fileName);
    }
}