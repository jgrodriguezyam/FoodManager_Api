namespace FoodManager.DTO.Message.Reservations
{
    public class ReservationCaloryReportResponse
    {
        public decimal Breakfast { get; set; }
        public decimal Lunch { get; set; }
        public decimal Dinner { get; set; }
        public string Date { get; set; }
    }
}