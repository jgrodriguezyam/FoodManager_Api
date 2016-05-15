using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.Reservations
{
    public class FindReservationsRequest : FindStatusRequest
    {
        public int WorkerId { get; set; }
        public int SaucerId { get; set; }
        public int DealerId { get; set; }
        public bool OnlyToday { get; set; }
    }
}