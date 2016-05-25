namespace FoodManager.DTO.Message.ReservationDetails
{
    public class ReservationDetailResponse
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int IngredientId { get; set; }
        public decimal Portion { get; set; }
    }
}