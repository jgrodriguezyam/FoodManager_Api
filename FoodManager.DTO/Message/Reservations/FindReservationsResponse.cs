using System.Collections.Generic;
using FoodManager.DTO.BaseResponse;

namespace FoodManager.DTO.Message.Reservations
{
    public class FindReservationsResponse : FindBaseResponse
    {
        public FindReservationsResponse()
        {
            Reservations = new List<ReservationResponse>();
        }

        public List<ReservationResponse> Reservations { get; set; }
    }
}