using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IReservationFactory
    {
        DTO.Reservation Execute(Reservation reservation);
    }
}