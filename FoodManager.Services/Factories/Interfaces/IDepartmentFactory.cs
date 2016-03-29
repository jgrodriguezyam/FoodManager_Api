using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IDepartmentFactory
    {
        DTO.Department Execute(Department department);
    }
}