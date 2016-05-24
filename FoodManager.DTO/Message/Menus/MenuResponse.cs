namespace FoodManager.DTO.Message.Menus
{
    public class MenuResponse
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int DayWeek { get; set; }
        public int MealType { get; set; }
        public int Limit { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int MaxAmount { get; set; }
        public int DealerId { get; set; }
        public int SaucerId { get; set; }
        public bool Status { get; set; }
    }
}