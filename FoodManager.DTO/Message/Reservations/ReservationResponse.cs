namespace FoodManager.DTO.Message.Reservations
{
    public class ReservationResponse
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int WorkerId { get; set; }
        public int SaucerId { get; set; }
        public int DealerId { get; set; }
        public decimal Portion { get; set; }
        public bool Status { get; set; }
    }
}