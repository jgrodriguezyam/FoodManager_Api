using System.Collections.Generic;

namespace FoodManager.Infrastructure.Bulks
{
    public interface IBulkQuery
    {
        bool Insert(IEnumerable<object> items);
    }
}