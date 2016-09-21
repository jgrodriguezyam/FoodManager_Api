namespace FoodManager.DTO.Message.Reservations
{
    public class ReservationReportRequest
    {
        public int WorkerId { get; set; }
        public string StarDate { get; set; }
        public string EndDate { get; set; }
    }
}