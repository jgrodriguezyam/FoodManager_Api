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

        public ReservationFactory(IWorkerRepository workerRepository, ISaucerRepository saucerRepository, INutritionInformationFactory nutritionInformationFactory)
        {
            _workerRepository = workerRepository;
            _saucerRepository = saucerRepository;
            _nutritionInformationFactory = nutritionInformationFactory;
        }

        public DTO.Reservation Execute(Reservation reservation)
        {
            var reservationDto = TypeAdapter.Adapt<DTO.Reservation>(reservation);
            var worker = _workerRepository.FindBy(reservation.WorkerId);
            reservationDto.Worker = TypeAdapter.Adapt<DTO.Worker>(worker);
            var saucer = _saucerRepository.FindBy(reservation.SaucerId);
            reservationDto.Saucer = TypeAdapter.Adapt<DTO.Saucer>(saucer);
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