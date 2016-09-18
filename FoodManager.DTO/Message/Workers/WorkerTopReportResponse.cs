using System.Collections.Generic;

namespace FoodManager.DTO.Message.Workers
{
    public class WorkerTopReportResponse
    {
        public int WorkerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ReservationCount { get; set; }
    }
}