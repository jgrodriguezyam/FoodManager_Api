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
        private readonly INutritionInformationFactory _nutritionInformationFactory;
        private readonly IDealerRepository _dealerRepository;

        public ReservationFactory(IWorkerRepository workerRepository, ISaucerRepository saucerRepository, INutritionInformationFactory nutritionInformationFactory, IDealerRepository dealerRepository)
        {
            _workerRepository = workerRepository;
            _saucerRepository = saucerRepository;
            _nutritionInformationFactory = nutritionInformationFactory;
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

        public void SetNutritionInformation(Reservation reservation)
        {
            var nutritionInformation = _nutritionInformationFactory.FindBySaucer(reservation.SaucerId);
            reservation.Energy = nutritionInformation.Energy;
            reservation.Protein = nutritionInformation.Protein;
            reservation.Carbohydrate = nutritionInformation.Carbohydrate;
            reservation.Sugar = nutritionInformation.Sugar;
            reservation.Lipid = nutritionInformation.Lipid;
            reservation.Sodium = nutritionInformation.Sodium;
        }
    }
}