using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericRepositories;

namespace FoodManager.Model.IRepositories
{
    public interface ITipRepository : IRepository<Tip>
    {
        void AddAll(IEnumerable<Tip> items);
    }
}