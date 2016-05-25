using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Strings;
using FoodManager.Infrastructure.Validators;

namespace FoodManager.Infrastructure.Queries
{
    public static class QueryFactory
    {
        public static string SelectQuery(this string query, string fields, string tables)
        {
            query += "SELECT " + fields + ", ROW_NUMBER() OVER (ORDER BY {0} {1}) rownum FROM " + tables;
            return query;
        }

        public static string SelectTotalQuery(this string query, string field, string tables)
        {
            query += string.Format("SELECT COUNT({0}) FROM {1}", field, tables);
            return query;
        }

        public static string SortResolver(this string sort)
        {
            if (sort.IsNotNullOrEmpty())
                return sort.ToUpper();
            return QueryConstants.OrderByAscending;
        }

        public static string Sort(this string query, string sort, string sortBy)
        {
            if (sort.IsNotNullOrEmpty() && (sort.Equals("ASC", StringComparison.OrdinalIgnoreCase) || sort.Equals("DESC", StringComparison.OrdinalIgnoreCase)))
                return string.Format(query, sortBy, sort);
            return string.Format(query, sortBy, "asc");
        }

        public static string AndCondition(this string query, string key, string value)
        {
            if (value.IsNotNullOrEmpty())
                query += query.Contains("WHERE") ? string.Format(" AND {0} = '{1}'", key, value) : string.Format(" WHERE {0} = '{1}'", key, value);
            return query;
        }

        public static string AndCondition(this string query, string key, int value)
        {
            if (value.IsGreaterThanZero())
                query = query.AndCondition(key, value.ToString());
            return query;
        }

        public static string TrueCondition(this string query, string key, bool onlyTrue)
        {
            if (onlyTrue)
                query = query.AndCondition(key, "1");
            return query;
        }

        public static string FalseCondition(this string query, string key, bool onlyFalse)
        {
            if (onlyFalse)
                query = query.AndCondition(key, "0");
            return query;
        }

        public static string LikeCondition(this string query, string key, string value)
        {
            if (value.IsNotNullOrEmpty())
                query += query.Contains("WHERE") ? string.Format(" AND UPPER({0}) LIKE '%{1}%'", key, value) : string.Format(" WHERE UPPER({0}) LIKE '%{1}%'", key, value);
            return query;
        }

        public static string IsNullCondition(this string query, string key, bool applyCondition)
        {
            if (applyCondition)
                query += query.Contains("WHERE") ? string.Format(" AND ({0} IS NULL OR {0} = '')", key) : string.Format(" WHERE ({0} IS NULL OR {0} = '')", key);
            return query;
        }

        public static string IsNotNullCondition(this string query, string key, bool applyCondition)
        {
            if (applyCondition)
                query += query.Contains("WHERE") ? string.Format(" AND ({0} IS NOT NULL OR {0} != '')", key) : string.Format(" WHERE ({0} IS NOT NULL OR {0} != '')", key);
            return query;
        }

        public static string BetweenDateCondition(this string query, string key, string firstValue, string secondValue)
        {
            if (firstValue.IsNotNullOrEmpty() && secondValue.IsNotNullOrEmpty())
                query += query.Contains("WHERE") ? string.Format(" AND CONVERT(DATE,{0},103) between CONVERT(DATE,'{1}',103) AND CONVERT(DATE,'{2}',103)", key, firstValue, secondValue) : string.Format(" WHERE CONVERT(DATE,{0},103) between CONVERT(DATE,'{1}',103) AND CONVERT(DATE,'{2}',103)", key, firstValue, secondValue);
            return query;
        }

        public static string Paginate(this string query, int startPage, int endPage, string fieldsToPaginate)
        {
            if (startPage.IsGreaterThanZero() && endPage.IsGreaterThanZero())
            {
                query = string.Format("SELECT {0} FROM ( {1} {2}" , fieldsToPaginate, query, ") seq WHERE seq.rownum BETWEEN {0} AND {1}");
                return string.Format(query, startPage, endPage);
            }
            return query;
        }

        public static string Paginate(this string query, int startPage, int endPage)
        {
            if (startPage.IsGreaterThanZero() && endPage.IsGreaterThanZero())
            {
                query = string.Format("SELECT * FROM ( {0} {1}", query, ") seq WHERE seq.rownum BETWEEN {0} AND {1}");
                return string.Format(query, startPage, endPage);
            }
            return query;
        }

        public static string SortByResolver<T>(this string sortBy, Dictionary<string, string> dictionary)
        {
            var checkProperty = Validate<T>.Property(sortBy);
            if (checkProperty)
                return dictionary[sortBy.ToLower()];
            return dictionary.Select(dic => dic.Value).First();
        }

        public static string SortByResolver<T>(this string sortBy)
        {
            var checkProperty = Validate<T>.Property(sortBy);
            if (checkProperty)
                return sortBy;
            return typeof(T).GetProperties().First().Name;
        }

        public static Expression<Func<T, object>> SortByPropertyResolver<T>(this string sortBy) where T : class
        {
            var checkProperty = Validate<T>.Property(sortBy);
            if (checkProperty.Equals(false))
                sortBy = typeof(T).GetProperties().First().Name;

            return Translation<T>.LambdaExpression(sortBy);
        }

        public static string InCondition(this string query, string key, string values)
        {
            if (values.IsNotNullOrEmpty())
                query += query.Contains("WHERE") ? string.Format(" AND {0} in ({1})", key, values) : string.Format(" WHERE {0} in ({1})", key, values);
            return query;
        }
        
        public static string SelectOnlyField(string table, string field)
        {
            return string.Format("SELECT {0} from {1}", field, table);
        }
        
        public static Expression<Func<T, bool>> DeleteMiddleLambdaResolver<T>(this T entityToDelete) where T : class
        {
            return Translation<T>.MiddleEntityLambdaExpression(entityToDelete);
        }
    }
}