using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Reservations;

namespace FoodManager.Services.Interfaces
{
    public interface IReservationService
    {
        FindReservationsResponse Find(FindReservationsRequest request);
        CreateResponse Create(ReservationRequest request);
        SuccessResponse Update(ReservationRequest request);
        ReservationResponse Get(GetReservationRequest request);
        SuccessResponse Delete(DeleteReservationRequest request);
        SuccessResponse ChangeStatus(ChangeStatusRequest request);
    }
}