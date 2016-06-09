using System.Collections.Generic;
using FoodManager.DTO.Message.ReservationDetails;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IReservationDetailFactory
    {
        ReservationDetailResponse Execute(ReservationDetail reservationDetail);
        IEnumerable<ReservationDetailResponse> Execute(IEnumerable<ReservationDetail> reservationDetails);
        List<ReservationDetail> BySaucer(int saucerId);
    }
}