using FoodManager.DTO.Message.Ingredients;
using FoodManager.DTO.Message.Reservations;

namespace FoodManager.DTO.Message.ReservationDetails
{
    public class ReservationDetailResponse
    {
        public int Id { get; set; }
        public ReservationResponse Reservation { get; set; }
        public IngredientResponse Ingredient { get; set; }
        public decimal Portion { get; set; }
    }
}