namespace FoodManager.DTO
{
    public class ReservationDetail
    {
        public int Id { get; set; }
        public Reservation Reservation { get; set; }
        public Ingredient Ingredient { get; set; }
        public decimal Portion { get; set; }
    }
}