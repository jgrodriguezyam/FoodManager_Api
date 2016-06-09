using System.Collections.Generic;
using FoodManager.Infrastructure.Files;
using FoodManager.Infrastructure.Strings;
using FoodManager.Model;
using FoodManager.Services.Factories.Interfaces;
using ServiceStack.Common.Extensions;

namespace FoodManager.Services.Factories.Implements
{
    public class SaucerFactory : ISaucerFactory
    {
        private readonly IStorageProvider _storageProvider;

        public SaucerFactory(IStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
        }

        public List<Saucer> FromCsv(string fileName)
        {
            var saucers = new List<Saucer>();
            var csvLines = _storageProvider.ReadAllLinesCsv(fileName);
            csvLines.ForEach(csvLine =>
                            {
                                var values = csvLine.Split(',');
                                saucers.Add(new Saucer
                                {
                                    Name = values[0],
                                    Type = values[1].ToInt()
                                });
                            });

            return saucers;
        }
    }
}