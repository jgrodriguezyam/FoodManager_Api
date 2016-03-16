using FoodManager.Infrastructure.IGenericRepositories;

namespace FoodManager.Model.IRepositories
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
        bool IsReference(int ingredientId);
    }
}