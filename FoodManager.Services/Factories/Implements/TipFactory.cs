using System.Collections.Generic;
using FoodManager.Infrastructure.Files;
using FoodManager.Model;
using FoodManager.Services.Factories.Interfaces;
using ServiceStack.Common.Extensions;

namespace FoodManager.Services.Factories.Implements
{
    public class TipFactory : ITipFactory
    {
        private readonly IStorageProvider _storageProvider;

        public TipFactory(IStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
        }

        public List<Tip> FromCsv(string fileName)
        {
            var tips = new List<Tip>();
            var csvLines = _storageProvider.ReadAllLinesCsv(fileName);
            csvLines.ForEach(csvLine =>
            {
                var values = csvLine.Split(',');
                tips.Add(new Tip
                {
                    Name = values[0]
                });
            });

            return tips;
        }
    }
}