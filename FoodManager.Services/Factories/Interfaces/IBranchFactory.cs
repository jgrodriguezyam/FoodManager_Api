using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IBranchFactory
    {
        DTO.Branch Execute(Branch branch);
    }
}