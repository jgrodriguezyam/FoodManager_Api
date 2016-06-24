using System.Web.Http;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Reservations;
using FoodManager.Infrastructure.Enums;
using FoodManager.Model.Enums;
using FoodManager.Services.Interfaces;

namespace FoodManager.Controllers
{
    public class ReservationController : ApiController
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet, Route("reservations")]
        public FindReservationsResponse Get(FindReservationsRequest request)
        {
            return _reservationService.Find(request);
        }

        [HttpPost, Route("reservations")]
        public CreateResponse Post(ReservationRequest request)
        {
            return _reservationService.Create(request);
        }

        [HttpPut, Route("reservations")]
        public SuccessResponse Put(ReservationRequest request)
        {
            return _reservationService.Update(request);
        }

        [HttpGet, Route("reservations/{Id}")]
        public ReservationResponse Get(GetReservationRequest request)
        {
            return _reservationService.Get(request);
        }

        [HttpDelete, Route("reservations/{Id}")]
        public SuccessResponse Delete(DeleteReservationRequest request)
        {
            return _reservationService.Delete(request);
        }

        [HttpPut, Route("reservations/{Id}/status/{Status}")]
        public SuccessResponse ChangeStatus(ChangeStatusRequest request)
        {
            return _reservationService.ChangeStatus(request);
        }

        [HttpGet, Route("reservations/meal-types")]
        public EnumeratorResponse Get()
        {
            return new EnumeratorResponse { Enumerator = new MealType().ConvertToCollection() };
        }
    }
}