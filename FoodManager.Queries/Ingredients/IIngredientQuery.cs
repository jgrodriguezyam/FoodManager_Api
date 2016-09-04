﻿using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericQuery;
using FoodManager.Model;

namespace FoodManager.Queries.Ingredients
{
    public interface IIngredientQuery : IQuery
    {
        void WithOnlyActivated(bool onlyActivated);
        void WithOnlyStatusActivated(bool onlyStatusActivated);
        void WithOnlyStatusDeactivated(bool onlyStatusDeactivated);
        void WithIngredientGroup(int ingredientGroupId);
        void WithName(string name);
        void WithoutIds(string ids);
        void WithUnit(int unit);
        void WithIds(string ids);
        IEnumerable<Ingredient> Execute();
    }
}