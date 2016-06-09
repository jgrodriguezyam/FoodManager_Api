using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Ingredients;
using FoodManager.DTO.Message.ReservationDetails;
using FoodManager.DTO.Message.Reservations;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class ReservationDetailFactory : IReservationDetailFactory
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly ISaucerConfigurationRepository _saucerConfigurationRepository;

        public ReservationDetailFactory(IReservationRepository reservationRepository, IIngredientRepository ingredientRepository, ISaucerConfigurationRepository saucerConfigurationRepository)
        {
            _reservationRepository = reservationRepository;
            _ingredientRepository = ingredientRepository;
            _saucerConfigurationRepository = saucerConfigurationRepository;
        }

        public ReservationDetailResponse Execute(ReservationDetail reservationDetail)
        {
            var reservationDetailResponse = TypeAdapter.Adapt<ReservationDetailResponse>(reservationDetail);
            var reservation = _reservationRepository.FindBy(reservationDetail.ReservationId);
            reservationDetailResponse.Reservation = TypeAdapter.Adapt<ReservationResponse>(reservation);
            var ingredient = _ingredientRepository.FindBy(reservationDetail.IngredientId);
            reservationDetailResponse.Ingredient = TypeAdapter.Adapt<IngredientResponse>(ingredient);
            return reservationDetailResponse;
        }

        public IEnumerable<ReservationDetailResponse> Execute(IEnumerable<ReservationDetail> reservationDetails)
        {
            return reservationDetails.Select(Execute);
        }

        public List<ReservationDetail> BySaucer(int saucerId)
        {
            var saucerConfigurations = _saucerConfigurationRepository.FindBy(saucerConfiguration => saucerConfiguration.SaucerId == saucerId && saucerConfiguration.Status);
            return TypeAdapter.Adapt<List<ReservationDetail>>(saucerConfigurations);
        }
    }
}