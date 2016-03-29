using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IWarningFactory
    {
        DTO.Warning Execute(Warning warning);
    }
}