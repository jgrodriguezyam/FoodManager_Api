using System.Collections.Generic;

namespace FoodManager.DTO.Message.Reservations
{
    public class ReservationReportResponse
    {
        public ReservationReportResponse()
        {
            Calories = new List<ReservationCaloryReportResponse>();
        }

        public List<ReservationCaloryReportResponse> Calories { get; set; }
    }
}