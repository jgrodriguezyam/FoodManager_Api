using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericQuery;
using FoodManager.Model;

namespace FoodManager.Queries.ReservationDetails
{
    public interface IReservationDetailQuery : IQuery
    {
        void WithReservation(int reservationId);
        IEnumerable<ReservationDetail> Execute();
    }
}