using System.Collections.Generic;
using FoodManager.DTO.Message.Branches;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IBranchFactory
    {
        BranchResponse Execute(Branch branch);
        IEnumerable<BranchResponse> Execute(IEnumerable<Branch> branches);
    }
}