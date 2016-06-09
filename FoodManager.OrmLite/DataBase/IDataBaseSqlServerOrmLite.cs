using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ServiceStack.OrmLite.SqlServer;

namespace FoodManager.OrmLite.DataBase
{
    public interface IDataBaseSqlServerOrmLite
    {
        void Insert<T>(T objectToInsert) where T : new();
        void InsertMiddleEntity<T>(T objectToInsert) where T : new();
        void Update<T>(T objectToUpdate) where T : new();
        void Remove<T>(T objectToRemove) where T : new();
        void Remove<T>(Expression<Func<T, bool>> predicate);
        void RemoveById<T>(int id) where T : new();
        void RemoveMiddleEntity<T>(T objectToRemove) where T : class;
        T GetById<T>(int id) where T : new();
        void LogicRemoveById<T>(int id) where T : new();
        void LogicRemove<T>(T objectToRemove) where T : new();
        T SingleById<T>(int id) where T : new();
        T SingleById<T>(string id) where T : new();
        IEnumerable<T> FindBy<T>(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindExpressionVisitor<T>(SqlServerExpressionVisitor<T> sqlExpressionVisitor) where T : new();
        int Count<T>(SqlServerExpressionVisitor<T> sqlExpressionVisitor) where T : new();
        int Count<T>(Expression<Func<T, bool>> predicate);
        void Commit();
        void Rollback();
        void UpdateHmac<T>(string publicKey, string time, int id) where T : new();
        T GetByIdOrDefault<T>(int id) where T : new();
        IEnumerable<T> FindAll<T>();
    }
}