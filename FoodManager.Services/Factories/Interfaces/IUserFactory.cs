using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IUserFactory
    {
        DTO.User Execute(User user);
    }
}