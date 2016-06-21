using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Saucers;
using FoodManager.Infrastructure.Files;
using FoodManager.Infrastructure.Strings;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;
using ServiceStack.Common.Extensions;

namespace FoodManager.Services.Factories.Implements
{
    public class SaucerFactory : ISaucerFactory
    {
        private readonly IStorageProvider _storageProvider;
        private readonly ISaucerRepository _saucerRepository;

        public SaucerFactory(IStorageProvider storageProvider, ISaucerRepository saucerRepository)
        {
            _storageProvider = storageProvider;
            _saucerRepository = saucerRepository;
        }

        public SaucerResponse Execute(Saucer saucer)
        {
            var saucerResponse = TypeAdapter.Adapt<SaucerResponse>(saucer);
            saucerResponse.IsReference = _saucerRepository.IsReference(saucer.Id);
            return saucerResponse;
        }

        public IEnumerable<SaucerResponse> Execute(IEnumerable<Saucer> saucers)
        {
            return saucers.Select(Execute);
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