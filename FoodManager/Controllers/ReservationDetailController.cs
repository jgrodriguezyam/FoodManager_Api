using System.Web.Http;
using FoodManager.DTO.Message.ReservationDetails;
using FoodManager.Services.Interfaces;

namespace FoodManager.Controllers
{
    public class ReservationDetailController : ApiController
    {
        private readonly IReservationDetailService _reservationDetailService;

        public ReservationDetailController(IReservationDetailService reservationDetailService)
        {
            _reservationDetailService = reservationDetailService;
        }

        [HttpGet, Route("reservation-details")]
        public FindReservationDetailsResponse Get(FindReservationDetailsRequest request)
        {
            return _reservationDetailService.Find(request);
        }

        [HttpGet, Route("reservation-details/{Id}")]
        public ReservationDetailResponse Get(GetReservationDetailRequest request)
        {
            return _reservationDetailService.Get(request);
        }
    }
}