using System.Collections.Generic;
using System.Linq;

namespace FoodManager.Infrastructure.Collections
{
    public static class CollectionExtensions
    {
        public static bool IsNotEmpty(this IEnumerable<object> values)
        {
            return values != null && values.Any();
        }

        public static bool IsEmpty(this IEnumerable<object> values)
        {
            return !IsNotEmpty(values);
        }
    }
}