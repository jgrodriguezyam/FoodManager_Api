using System.Collections.Generic;
using FoodManager.DTO.Message.Roles;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IRoleFactory
    {
        RoleResponse Execute(Role role);
        IEnumerable<RoleResponse> Execute(IEnumerable<Role> roles);
    }
}