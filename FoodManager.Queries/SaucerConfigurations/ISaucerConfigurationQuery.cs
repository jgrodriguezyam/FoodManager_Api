using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericQuery;
using FoodManager.Model;

namespace FoodManager.Queries.SaucerConfigurations
{
    public interface ISaucerConfigurationQuery : IQuery
    {
        void WithOnlyActivated(bool onlyActivated);
        void WithOnlyStatusActivated(bool onlyStatusActivated);
        void WithOnlyStatusDeactivated(bool onlyStatusDeactivated);
        void WithSaucer(int saucerId);
        void WithSaucerIds(string saucerIds);
        void WithIngredient(int ingredientId);
        IEnumerable<SaucerConfiguration> Execute();
    }
}