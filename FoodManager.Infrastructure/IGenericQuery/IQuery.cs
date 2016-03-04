using System.Collections.Generic;

namespace FoodManager.Infrastructure.IGenericQuery
{
    public interface IQuery
    {
        void Sort(string sort, string sortBy);
        void Paginate(int startPage, int endPage);
        int TotalRecords();
        //IEnumerable<T> Execute<T>(out int totalRecords);
    }
}