using FoodManager.Infrastructure.Application;

namespace FoodManager.DTO.Message.Reservations
{
    public class ReservationResponse : NutritionInformation
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int WorkerId { get; set; }
        public int SaucerId { get; set; }
        public int DealerId { get; set; }
        public bool Status { get; set; }
    }
}