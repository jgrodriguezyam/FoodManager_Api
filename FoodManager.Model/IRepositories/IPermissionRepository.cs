﻿using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericRepositories;

namespace FoodManager.Model.IRepositories
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        IEnumerable<Permission> FindAll();
    }
}