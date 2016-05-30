using System.Collections.Generic;
using FoodManager.Infrastructure.Files;
using FoodManager.Model;
using FoodManager.Services.Factories.Interfaces;
using ServiceStack.Common.Extensions;

namespace FoodManager.Services.Factories.Implements
{
    public class IngredientGroupFactory : IIngredientGroupFactory
    {
        private readonly IStorageProvider _storageProvider;

        public IngredientGroupFactory(IStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
        }

        public List<IngredientGroup> FromCsv(string fileName)
        {
            var ingredientGroups = new List<IngredientGroup>();
            var csvLines = _storageProvider.ReadAllLinesCsv(fileName);
            csvLines.ForEach(csvLine =>
                             {
                             var values = csvLine.Split(',');
                             ingredientGroups.Add(new IngredientGroup
                                 {
                                     Name = values[0],
                                     Color = values[1]
                                 });
                             });
                                           
            return ingredientGroups;
        }
    }
}