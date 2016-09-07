using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Saucers;
using FoodManager.Infrastructure.Files;
using FoodManager.Infrastructure.Integers;
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
        private readonly IMenuRepository _menuRepository;
        private readonly IReservationRepository _reservationRepository;

        public SaucerFactory(IStorageProvider storageProvider, IMenuRepository menuRepository, IReservationRepository reservationRepository)
        {
            _storageProvider = storageProvider;
            _menuRepository = menuRepository;
            _reservationRepository = reservationRepository;
        }

        public SaucerResponse Execute(Saucer saucer)
        {
            return AppendProperties(new[] { saucer }).FirstOrDefault();
        }

        public IEnumerable<SaucerResponse> Execute(IEnumerable<Saucer> saucers)
        {
            return AppendProperties(saucers);
        }

        private IEnumerable<SaucerResponse> AppendProperties(IEnumerable<Saucer> saucers)
        {
            var saucersResponse = TypeAdapter.Adapt<List<SaucerResponse>>(saucers);
            var menus = _menuRepository.FindBy(menu => menu.IsActive);
            var reservations = _reservationRepository.FindBy(menu => menu.IsActive);

            saucersResponse.ForEach(saucerResponse =>
            {
                var saucer = saucers.First(saucerModel => saucerModel.Id == saucerResponse.Id);
                var amountOfReferences = menus.Count(menu => menu.SaucerId == saucer.Id);
                amountOfReferences += reservations.Count(reservation => reservation.SaucerId == saucer.Id);
                saucerResponse.IsReference = amountOfReferences.IsNotZero();
            });

            return saucersResponse;
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