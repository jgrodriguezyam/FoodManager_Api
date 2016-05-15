using FoodManager.Infrastructure.Application;

namespace FoodManager.DTO
{
    public class Reservation : NutritionInformation
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public Worker Worker { get; set; }
        public Saucer Saucer { get; set; }
        public Dealer Dealer { get; set; }
        public bool Status { get; set; }
    }
}