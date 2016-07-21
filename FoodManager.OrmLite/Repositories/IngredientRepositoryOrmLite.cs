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
    public class IngredientRepositoryOrmLite : IIngredientRepository
    {
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;
        private readonly IAuditEventListener _auditEventListener;

        public IngredientRepositoryOrmLite(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite, IAuditEventListener auditEventListener)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _auditEventListener = auditEventListener;
        }

        public Ingredient FindBy(int id)
        {
            return _dataBaseSqlServerOrmLite.GetById<Ingredient>(id);
        }

        public IEnumerable<Ingredient> FindBy(Expression<Func<Ingredient, bool>> predicate)
        {
            return _dataBaseSqlServerOrmLite.FindBy(predicate);
        }

        public void Add(Ingredient item)
        {
            _auditEventListener.OnPreInsert(item);
            _dataBaseSqlServerOrmLite.Insert(item);
        }

        public void Update(Ingredient item)
        {
            _auditEventListener.OnPreUpdate(item);
            _dataBaseSqlServerOrmLite.Update(item);
        }

        public void Remove(Ingredient item)
        {
            _auditEventListener.OnPreDelete(item);
            _dataBaseSqlServerOrmLite.LogicRemove(item);
        }

        public bool IsReference(int ingredientId)
        {
            var amountOfReferences = _dataBaseSqlServerOrmLite.Count<SaucerConfiguration>(saucerConfiguration => saucerConfiguration.IngredientId == ingredientId && saucerConfiguration.IsActive);
            amountOfReferences += _dataBaseSqlServerOrmLite.Count<ReservationDetail>(reservationDetail => reservationDetail.IngredientId == ingredientId && reservationDetail.IsActive);
            return amountOfReferences.IsNotZero();
        }

        public void AddAll(IEnumerable<Ingredient> items)
        {
            items.ForEach(item => { _auditEventListener.OnPreInsert(item); });
            _dataBaseSqlServerOrmLite.InsertAll(items);
        }
    }
}