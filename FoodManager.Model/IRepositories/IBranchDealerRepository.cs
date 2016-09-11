using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericRepositories;

namespace FoodManager.Model.IRepositories
{
    public interface IBranchDealerRepository : IRepository<BranchDealer>
    {
        IEnumerable<BranchDealer> FindAll();
    }
}