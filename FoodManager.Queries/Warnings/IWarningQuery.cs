using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericQuery;
using FoodManager.Model;

namespace FoodManager.Queries.Warnings
{
    public interface IWarningQuery : IQuery
    {
        void WithOnlyActivated(bool onlyActivated);
        IEnumerable<Warning> Execute();
    }
}