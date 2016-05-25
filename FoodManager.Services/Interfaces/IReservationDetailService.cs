using FoodManager.DTO;
using FoodManager.DTO.Message.ReservationDetails;

namespace FoodManager.Services.Interfaces
{
    public interface IReservationDetailService
    {
        FindReservationDetailsResponse Find(FindReservationDetailsRequest request);
        ReservationDetail Get(GetReservationDetailRequest request);
    }
}