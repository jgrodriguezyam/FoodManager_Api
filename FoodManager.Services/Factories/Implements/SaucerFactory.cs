using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Saucers;
using FoodManager.Infrastructure.Enums;
using FoodManager.Infrastructure.Files;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Strings;
using FoodManager.Model;
using FoodManager.Model.Enums;
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
        private readonly ISaucerRepository _saucerRepository;
        private readonly int _mainSaucerTop = 10;
        private readonly int _garrisonSaucerTop = 10;

        public SaucerFactory(IStorageProvider storageProvider, IMenuRepository menuRepository, IReservationRepository reservationRepository, ISaucerRepository saucerRepository)
        {
            _storageProvider = storageProvider;
            _menuRepository = menuRepository;
            _reservationRepository = reservationRepository;
            _saucerRepository = saucerRepository;
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

        public IEnumerable<SaucerTopReportResponse> MainSaucersExecute(SaucerReportRequest saucerReportRequest)
        {
            var saucers = _saucerRepository.FindBy(saucer => saucer.IsActive);
            var saucerIds = saucers.Where(saucer => saucer.Type == SaucerType.Main.GetValue()).Select(saucer => saucer.Id);
            var reservations = _reservationRepository.FindBy(reservation => reservation.IsActive && saucerIds.Contains(reservation.SaucerId));
            var reservationsGroup = reservations.GroupBy(reservation => reservation.SaucerId);

            var saucersTop = reservationsGroup.Select(reservationGroup => new SaucerTopReportResponse
                                {
                                    SaucerId = reservationGroup.Key,
                                    Count = reservationGroup.Count()
                                })
                                .OrderByDescending(saucerTop => saucerTop.Count).Take(_mainSaucerTop).ToList();

            saucersTop.ForEach(saucerTop =>
            {
                var saucer = saucers.First(currentSaucer => currentSaucer.Id == saucerTop.SaucerId);
                saucerTop.Name = saucer.Name;
            });

            return saucersTop;
        }

        public IEnumerable<SaucerTopReportResponse> GarrisonSaucersExecute(SaucerReportRequest saucerReportRequest)
        {
            var saucers = _saucerRepository.FindBy(saucer => saucer.IsActive);
            var saucerIds = saucers.Where(saucer => saucer.Type == SaucerType.Garrison.GetValue()).Select(saucer => saucer.Id);
            var reservations = _reservationRepository.FindBy(reservation => reservation.IsActive && saucerIds.Contains(reservation.SaucerId));
            var reservationsGroup = reservations.GroupBy(reservation => reservation.SaucerId);

            var saucersTop = reservationsGroup.Select(reservationGroup => new SaucerTopReportResponse
                                {
                                    SaucerId = reservationGroup.Key,
                                    Count = reservationGroup.Count()
                                })
                                .OrderByDescending(saucerTop => saucerTop.Count).Take(_garrisonSaucerTop).ToList();

            saucersTop.ForEach(saucerTop =>
            {
                var saucer = saucers.First(currentSaucer => currentSaucer.Id == saucerTop.SaucerId);
                saucerTop.Name = saucer.Name;
            });

            return saucersTop;
        }
    }
}