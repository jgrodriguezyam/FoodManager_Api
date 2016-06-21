using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.ReservationDetails;

namespace FoodManager.Services.Interfaces
{
    public interface IReservationDetailService
    {
        FindReservationDetailsResponse Find(FindReservationDetailsRequest request);
        ReservationDetailResponse Get(GetReservationDetailRequest request);
        SuccessResponse DeleteByParent(DeleteByParentRequest request);
    }
}