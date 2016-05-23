using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericQuery;
using FoodManager.Model;

namespace FoodManager.Queries.Reservations
{
    public interface IReservationQuery : IQuery
    {
        void WithOnlyActivated(bool onlyActivated);
        void WithOnlyStatusActivated(bool onlyStatusActivated);
        void WithOnlyStatusDeactivated(bool onlyStatusDeactivated);
        void WithWorker(int workerId);
        void WithSaucer(int saucerId);
        void WithDealer(int dealerId);
        void WithOnlyToday(bool onlyToday);
        void WithDate(string date);
        void WithPortion(decimal portion);
        IEnumerable<Reservation> Execute();
    }
}