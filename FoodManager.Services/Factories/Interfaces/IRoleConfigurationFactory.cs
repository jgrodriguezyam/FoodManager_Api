using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IRoleConfigurationFactory
    {
        DTO.RoleConfiguration Execute(RoleConfiguration roleConfiguration);
    }
}