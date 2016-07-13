using FoodManager.DTO.Message.Dealers;
using FoodManager.DTO.Message.Saucers;
using FoodManager.DTO.Message.Workers;

namespace FoodManager.DTO.Message.Reservations
{
    public class ReservationResponse
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public WorkerResponse Worker { get; set; }
        public SaucerResponse Saucer { get; set; }
        public DealerResponse Dealer { get; set; }
        public decimal Portion { get; set; }
        public int MealType { get; set; }
        public bool IsPaid { get; set; }
        public bool Status { get; set; }
    }
}