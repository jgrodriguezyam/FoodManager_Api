using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodManager.Infrastructure.Enums
{
    public static class EnumExtensions
    {
        public static List<Enumerator> ConvertToCollection(this Enum enumerator)
        {
            var type = enumerator.GetType();
            return Enum.GetNames(type)
                    .Select(name => new Enumerator { Name = name, Value = (int)Enum.Parse(type, name) })
                    .ToList();
        }

        public static int GetValue(this object key)
        {
            var type = key.GetType();
            return (int)Enum.Parse(type, key.ToString()); ;
        }

        public static int GetValue(this Enum enumerator, string key)
        {
            var type = enumerator.GetType();
            return (int)Enum.Parse(type, key);
        }
    }
}