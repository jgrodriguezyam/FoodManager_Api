using FoodManager.DTO.Message.Dealers;
using FoodManager.DTO.Message.Saucers;

namespace FoodManager.DTO.Message.Menus
{
    public class MenuResponse
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int DayWeek { get; set; }
        public int MealType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int MaxAmount { get; set; }
        public DealerResponse Dealer { get; set; }
        public SaucerResponse Saucer { get; set; }
        public bool Status { get; set; }
    }
}