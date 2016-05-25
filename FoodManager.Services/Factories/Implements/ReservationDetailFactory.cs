using System.Collections.Generic;
using FastMapper;
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

        public DTO.ReservationDetail Execute(ReservationDetail reservationDetail)
        {
            var reservationDetailDto = TypeAdapter.Adapt<DTO.ReservationDetail>(reservationDetail);
            var reservation = _reservationRepository.FindBy(reservationDetail.ReservationId);
            reservationDetailDto.Reservation = TypeAdapter.Adapt<DTO.Reservation>(reservation);
            var ingredient = _ingredientRepository.FindBy(reservationDetail.IngredientId);
            reservationDetailDto.Ingredient = TypeAdapter.Adapt<DTO.Ingredient>(ingredient);
            return reservationDetailDto;
        }

        public List<ReservationDetail> BySaucer(int saucerId)
        {
            var saucerConfigurations = _saucerConfigurationRepository.FindBy(saucerConfiguration => saucerConfiguration.SaucerId == saucerId && saucerConfiguration.Status);
            return TypeAdapter.Adapt<List<ReservationDetail>>(saucerConfigurations);
        }
    }
}