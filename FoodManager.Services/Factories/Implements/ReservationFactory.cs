using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Dealers;
using FoodManager.DTO.Message.Reservations;
using FoodManager.DTO.Message.Saucers;
using FoodManager.DTO.Message.Workers;
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
            var reservationResponse = TypeAdapter.Adapt<ReservationResponse>(reservation);
            var worker = _workerRepository.FindBy(reservation.WorkerId);
            reservationResponse.Worker = TypeAdapter.Adapt<WorkerResponse>(worker);
            var saucer = _saucerRepository.FindBy(reservation.SaucerId);
            reservationResponse.Saucer = TypeAdapter.Adapt<SaucerResponse>(saucer);
            var dealer = _dealerRepository.FindBy(reservation.DealerId);
            reservationResponse.Dealer = TypeAdapter.Adapt<DealerResponse>(dealer);
            return reservationResponse;
        }

        public IEnumerable<ReservationResponse> Execute(IEnumerable<Reservation> reservations)
        {
            return reservations.Select(Execute);
        }
    }
}