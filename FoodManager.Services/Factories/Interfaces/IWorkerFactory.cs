using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IWorkerFactory
    {
        DTO.Worker Execute(Worker worker);
    }
}