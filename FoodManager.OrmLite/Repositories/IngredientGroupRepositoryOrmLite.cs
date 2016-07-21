using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FoodManager.DataAccess.Listeners;
using FoodManager.Infrastructure.Integers;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.OrmLite.DataBase;
using ServiceStack.Common.Extensions;

namespace FoodManager.OrmLite.Repositories
{
    public class IngredientGroupRepositoryOrmLite : IIngredientGroupRepository
    {
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;
        private readonly IAuditEventListener _auditEventListener;

        public IngredientGroupRepositoryOrmLite(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite, IAuditEventListener auditEventListener)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _auditEventListener = auditEventListener;
        }

        public IngredientGroup FindBy(int id)
        {
            return _dataBaseSqlServerOrmLite.GetById<IngredientGroup>(id);
        }

        public IEnumerable<IngredientGroup> FindBy(Expression<Func<IngredientGroup, bool>> predicate)
        {
            return _dataBaseSqlServerOrmLite.FindBy(predicate);
        }

        public void Add(IngredientGroup item)
        {
            _auditEventListener.OnPreInsert(item);
            _dataBaseSqlServerOrmLite.Insert(item);
        }

        public void Update(IngredientGroup item)
        {
            _auditEventListener.OnPreUpdate(item);
            _dataBaseSqlServerOrmLite.Update(item);
        }

        public void Remove(IngredientGroup item)
        {
            _auditEventListener.OnPreDelete(item);
            _dataBaseSqlServerOrmLite.LogicRemove(item);
        }

        public bool IsReference(int ingredientGroupId)
        {
            var amountOfReferences = _dataBaseSqlServerOrmLite.Count<Ingredient>(ingredient => ingredient.IngredientGroupId == ingredientGroupId && ingredient.IsActive);
            return amountOfReferences.IsNotZero();
        }

        public void AddAll(IEnumerable<IngredientGroup> items)
        {
            items.ForEach(item => { _auditEventListener.OnPreInsert(item); });
            _dataBaseSqlServerOrmLite.InsertAll(items);
        }
    }
}