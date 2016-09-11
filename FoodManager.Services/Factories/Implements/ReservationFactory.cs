using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Dealers;
using FoodManager.DTO.Message.Reservations;
using FoodManager.DTO.Message.Saucers;
using FoodManager.DTO.Message.Workers;
using FoodManager.Infrastructure.Integers;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class ReservationFactory : IReservationFactory
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly ISaucerRepository _saucerRepository;
        private readonly IDealerRepository _dealerRepository;
        
        public ReservationFactory(IWorkerRepository workerRepository, ISaucerRepository saucerRepository, IDealerRepository dealerRepository)
        {
            _workerRepository = workerRepository;
            _saucerRepository = saucerRepository;
            _dealerRepository = dealerRepository;
        }

        public ReservationResponse Execute(Reservation reservation)
        {
            return AppendProperties(new[] { reservation }).FirstOrDefault();
        }

        public IEnumerable<ReservationResponse> Execute(IEnumerable<Reservation> reservations)
        {
            return AppendProperties(reservations);
        }

        private IEnumerable<ReservationResponse> AppendProperties(IEnumerable<Reservation> reservations)
        {
            var reservationsResponse = TypeAdapter.Adapt<List<ReservationResponse>>(reservations);
            var workers = _workerRepository.FindBy(worker => worker.IsActive);
            var saucers = _saucerRepository.FindBy(saucer => saucer.IsActive);
            var dealers = _dealerRepository.FindBy(dealer => dealer.IsActive);

            reservationsResponse.ForEach(reservationResponse =>
            {
                var reservation = reservations.First(reservationModel => reservationModel.Id == reservationResponse.Id);
                var worker = workers.First(workerModel => workerModel.Id == reservation.WorkerId);
                reservationResponse.Worker = TypeAdapter.Adapt<WorkerResponse>(worker);
                var saucer = saucers.First(saucerModel => saucerModel.Id == reservation.SaucerId);
                reservationResponse.Saucer = TypeAdapter.Adapt<SaucerResponse>(saucer);
                var dealerId = reservation.DealerId ?? 0;
                if (dealerId.IsNotZero())
                {
                    var dealer = dealers.First(dealerModel => dealerModel.Id == dealerId);
                    reservationResponse.Dealer = TypeAdapter.Adapt<DealerResponse>(dealer);
                }
            });

            return reservationsResponse;
        }
    }
}