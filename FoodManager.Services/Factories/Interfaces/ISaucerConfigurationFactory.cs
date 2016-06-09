using System.Collections.Generic;
using FoodManager.DTO.Message.SaucerConfigurations;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface ISaucerConfigurationFactory
    {
        SaucerConfigurationResponse Execute(SaucerConfiguration saucerConfiguration);
        IEnumerable<SaucerConfigurationResponse> Execute(IEnumerable<SaucerConfiguration> saucerConfigurations);
    }
}