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

        public static Expression<Func<T, bool>> MiddleEntityLambdaExpression(T item)
        {
            var firstPropertyName = typeof(T).GetProperties()[0].Name;
            var firstPropertyValue = item.GetType().GetProperty(firstPropertyName).GetValue(item, null);
            var secondPropertyName = typeof(T).GetProperties()[1].Name;
            var secondPropertyValue = item.GetType().GetProperty(secondPropertyName).GetValue(item, null);
            var paramExpression = Expression.Parameter(typeof(T), "s");
            
            var firstProperty = Expression.Property(paramExpression, firstPropertyName);
            var firstValue = Expression.Parameter(typeof(int), firstPropertyValue.ToString());
            var firstConditionEqual = Expression.Equal(firstProperty, firstValue);

            var secondProperty = Expression.Property(paramExpression, secondPropertyName);
            var secondValue = Expression.Parameter(typeof(int), secondPropertyValue.ToString());
            var secondConditionEqual = Expression.Equal(secondProperty, secondValue);

            var concatConditions = Expression.And(firstConditionEqual, secondConditionEqual);
            Expression conversion = Expression.Convert(concatConditions, typeof(bool));
            var lambda = Expression.Lambda<Func<T, bool>>(conversion, paramExpression);
            return lambda;
        }
    }
}