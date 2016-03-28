using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using FoodManager.Infrastructure.Queries;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;

namespace FoodManager.OrmLite.DataBase
{
    public class DataBaseSqlServerOrmLite : IDataBaseSqlServerOrmLite
    {
        private const int RegisterDeactivate = 0;
        private const int RegisterActivate = 1;
        public IDbConnectionFactory DbConnectionFactory;
        public IDbConnection DbConnection;
        public IDbTransaction DbTransaction;

        public void Insert<T>(T objectToInsert) where T : new()
        {
            DbConnection.Insert(objectToInsert);
            var lastInsertId = DbConnection.GetLastInsertId();
            objectToInsert.GetType().GetProperty("Id").SetValue(objectToInsert, Convert.ToInt32(lastInsertId), null);
        }

        public void InsertMiddleEntity<T>(T objectToInsert) where T : new()
        {
            DbConnection.Insert(objectToInsert);
        }

        public void Update<T>(T objectToUpdate) where T : new()
        {
            DbConnection.Update(objectToUpdate);
        }

        public void Remove<T>(T objectToRemove) where T : new()
        {
            DbConnection.Delete(objectToRemove);
        }

        public void Remove<T>(Expression<Func<T, bool>> predicate)
        {
            DbConnection.Delete(predicate);
        }

        public void RemoveById<T>(int id) where T : new()
        {
            DbConnection.DeleteById<T>(id);
        }

        public void RemoveMiddleEntity<T>(T objectToRemove) where T : class
        {
            var predicate = objectToRemove.DeleteMiddleLambdaResolver();
            DbConnection.Delete<T>(predicate);
        }

        public T GetById<T>(int id) where T : new()
        {
            var item = DbConnection.GetByIdOrDefault<T>(id);
            return item;
        }

        public void LogicRemoveById<T>(int id) where T : new()
        {
            DbConnection.Update<T>("IsActive = {0}".Params(RegisterDeactivate), "Id = {0}".Params(id));
        }

        public void LogicRemove<T>(T objectToRemove) where T : new()
        {
            DbConnection.Update(objectToRemove);
        }

        public T SingleById<T>(int id) where T : new()
        {
            var filter = string.Format("Status = {0} AND Id = {1}", RegisterActivate, id);
            var item = DbConnection.SingleOrDefault<T>(filter);
            return item;
        }

        public T SingleById<T>(string id) where T : new()
        {
            var filter = string.Format("Status = {0} AND Id = '{1}'", RegisterActivate, id);
            var item = DbConnection.SingleOrDefault<T>(filter);
            return item;
        }

        public IEnumerable<T> FindBy<T>(Expression<Func<T, bool>> predicate)
        {
            var items = DbConnection.Select(predicate);
            return items;
        }

        public IEnumerable<T> FindExpressionVisitor<T>(SqlServerExpressionVisitor<T> sqlExpressionVisitor) where T : new()
        {
            var items = DbConnection.Select(sqlExpressionVisitor);
            return items;
        }

        public int Count<T>(SqlServerExpressionVisitor<T> sqlExpressionVisitor) where T : new()
        {
            var totalCount = DbConnection.Count(sqlExpressionVisitor);
            return Convert.ToInt32(totalCount);
        }

        public int Count<T>(Expression<Func<T, bool>> predicate)
        {
            var totalCount = DbConnection.Count(predicate);
            return Convert.ToInt32(totalCount);
        }

        public void Commit()
        {
            DbTransaction.Commit();
            DbConnection.Dispose();
        }

        public void Rollback()
        {
            DbTransaction.Rollback();
            DbConnection.Dispose();
        }

        public void UpdateHmac<T>(string publicKey, string time, int id) where T : new()
        {
            var fieldsToUpdate = string.Format("PublicKey = '{0}', Time = '{1}'", publicKey, time);
            DbConnection.Update<T>(fieldsToUpdate, "Id = {0}".Params(id));
        }
    }
}
