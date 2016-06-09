using System.Collections.Generic;
using FoodManager.DTO.Message.Users;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IUserFactory
    {
        UserResponse Execute(User user);
        IEnumerable<UserResponse> Execute(IEnumerable<User> users);
    }
}