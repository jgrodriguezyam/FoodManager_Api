using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface ISaucerConfigurationFactory
    {
        DTO.SaucerConfiguration Execute(SaucerConfiguration saucerConfiguration);
    }
}