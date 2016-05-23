namespace FoodManager.DTO
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public Worker Worker { get; set; }
        public Saucer Saucer { get; set; }
        public Dealer Dealer { get; set; }
        public decimal Portion { get; set; }
        public bool Status { get; set; }
    }
}