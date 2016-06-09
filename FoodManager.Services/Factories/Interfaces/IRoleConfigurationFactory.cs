using System.Collections.Generic;
using FoodManager.DTO.Message.RoleConfigurations;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IRoleConfigurationFactory
    {
        RoleConfigurationResponse Execute(RoleConfiguration roleConfiguration);
        IEnumerable<RoleConfigurationResponse> Execute(IEnumerable<RoleConfiguration> roleConfigurations);
    }
}