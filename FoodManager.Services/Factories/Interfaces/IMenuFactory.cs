using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IMenuFactory
    {
        DTO.Menu Execute(Menu menu);
    }
}