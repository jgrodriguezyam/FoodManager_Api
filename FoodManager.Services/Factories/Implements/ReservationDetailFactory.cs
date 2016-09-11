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
            return AppendProperties(new[] { reservationDetail }).FirstOrDefault();
        }

        public IEnumerable<ReservationDetailResponse> Execute(IEnumerable<ReservationDetail> reservationDetails)
        {
            return AppendProperties(reservationDetails);
        }

        private IEnumerable<ReservationDetailResponse> AppendProperties(IEnumerable<ReservationDetail> reservationDetails)
        {
            var reservationDetailsResponse = TypeAdapter.Adapt<List<ReservationDetailResponse>>(reservationDetails);
            var reservations = _reservationRepository.FindBy(reservation => reservation.IsActive);
            var ingredients = _ingredientRepository.FindBy(ingredient => ingredient.IsActive);

            reservationDetailsResponse.ForEach(reservationDetailResponse =>
            {
                var reservationDetail = reservationDetails.First(reservationDetailModel => reservationDetailModel.Id == reservationDetailResponse.Id);
                var reservation = reservations.First(reservationModel => reservationModel.Id == reservationDetail.ReservationId);
                reservationDetailResponse.Reservation = TypeAdapter.Adapt<ReservationResponse>(reservation);
                var ingredient = ingredients.First(ingredientModel => ingredientModel.Id == reservationDetail.IngredientId);
                reservationDetailResponse.Ingredient = TypeAdapter.Adapt<IngredientResponse>(ingredient);
            });

            return reservationDetailsResponse;
        }

        public List<ReservationDetail> BySaucer(int saucerId)
        {
            var saucerConfigurations = _saucerConfigurationRepository.FindBy(saucerConfiguration => saucerConfiguration.SaucerId == saucerId && saucerConfiguration.Status);
            return TypeAdapter.Adapt<List<ReservationDetail>>(saucerConfigurations);
        }
    }
}