using System.Collections.Generic;
using FoodManager.DTO.Message.Reservations;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IReservationFactory
    {
        ReservationResponse Execute(Reservation reservation);
        IEnumerable<ReservationResponse> Execute(IEnumerable<Reservation> reservations);
    }
}