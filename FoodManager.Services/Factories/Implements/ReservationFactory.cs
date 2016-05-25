using FastMapper;
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

        public DTO.Reservation Execute(Reservation reservation)
        {
            var reservationDto = TypeAdapter.Adapt<DTO.Reservation>(reservation);
            var worker = _workerRepository.FindBy(reservation.WorkerId);
            reservationDto.Worker = TypeAdapter.Adapt<DTO.Worker>(worker);
            var saucer = _saucerRepository.FindBy(reservation.SaucerId);
            reservationDto.Saucer = TypeAdapter.Adapt<DTO.Saucer>(saucer);
            var dealer = _dealerRepository.FindBy(reservation.DealerId);
            reservationDto.Dealer = TypeAdapter.Adapt<DTO.Dealer>(dealer);
            return reservationDto;
        }
    }
}