using System.Collections.Generic;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IReservationDetailFactory
    {
        DTO.ReservationDetail Execute(ReservationDetail reservationDetail);
        List<ReservationDetail> BySaucer(int saucerId);
    }
}