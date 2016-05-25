using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.ReservationDetails
{
    public class FindReservationDetailsRequest : FindBaseRequest
    {
        public int ReservationId { get; set; }
    }
}