﻿using FoodManager.Infrastructure.IGenericRepositories;

namespace FoodManager.Model.IRepositories
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        bool IsReference(int reservationId);
    }
}