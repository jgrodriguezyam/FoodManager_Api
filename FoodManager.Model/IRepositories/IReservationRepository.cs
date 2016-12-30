using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericRepositories;

namespace FoodManager.Model.IRepositories
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        IEnumerable<Reservation> FindAll();
    }
}