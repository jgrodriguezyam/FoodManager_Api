using System;
using System.Linq.Expressions;

namespace FoodManager.Infrastructure.Queries
{
    public static class Translation<T> where T : class
    {
        public static Expression<Func<T, object>> LambdaExpression(string property)
        {
            ParameterExpression argParam = Expression.Parameter(typeof(T), "s");
            Expression nameProperty = Expression.Property(argParam, property);
            Expression conversion = Expression.Convert(nameProperty, typeof(object));
            var lambda = Expression.Lambda<Func<T, object>>(conversion, argParam);
            return lambda;
        }
    }
}