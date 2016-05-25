using System.Collections.Generic;
using FoodManager.DTO.BaseResponse;

namespace FoodManager.DTO.Message.ReservationDetails
{
    public class FindReservationDetailsResponse : FindBaseResponse
    {
        public FindReservationDetailsResponse()
        {
            ReservationDetails = new List<ReservationDetailResponse>();
        }

        public List<ReservationDetailResponse> ReservationDetails { get; set; }
    }
}