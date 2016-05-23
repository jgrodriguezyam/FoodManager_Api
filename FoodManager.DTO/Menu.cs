namespace FoodManager.DTO
{
    public class Menu
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int DayWeek { get; set; }
        public int Type { get; set; }
        public int Limit { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int MaxAmount { get; set; }
        public Dealer Dealer { get; set; }
        public Saucer Saucer { get; set; }
    }
}