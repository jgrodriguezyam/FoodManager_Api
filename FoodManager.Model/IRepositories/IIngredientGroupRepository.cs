using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericRepositories;

namespace FoodManager.Model.IRepositories
{
    public interface IIngredientGroupRepository : IRepository<IngredientGroup>
    {
        bool IsReference(int ingredientGroupId);
        void AddAll(IEnumerable<IngredientGroup> items);
    }
}