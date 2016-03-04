﻿using FoodManager.Infrastructure.IGenericRepositories;

namespace FoodManager.Model.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        User FindBy(string userName, string password);
    }
}