using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Dealers;
using FoodManager.DTO.Message.Roles;
using FoodManager.DTO.Message.Users;
using FoodManager.Infrastructure.Objects;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class UserFactory : IUserFactory
    {
        private readonly IDealerRepository _dealerRepository;
        private readonly IRoleRepository _roleRepository;

        public UserFactory(IDealerRepository dealerRepository, IRoleRepository roleRepository)
        {
            _dealerRepository = dealerRepository;
            _roleRepository = roleRepository;
        }

        public UserResponse Execute(User user)
        {
            var userResponse = TypeAdapter.Adapt<UserResponse>(user);
            var dealerId = user.DealerId ?? 0;
            var dealer = _dealerRepository.FindBy(dealerId);
            if (dealer.IsNotNull())
                userResponse.Dealer = TypeAdapter.Adapt<DealerResponse>(dealer);
            var role = _roleRepository.FindBy(user.RoleId);
            userResponse.Role = TypeAdapter.Adapt<RoleResponse>(role);
            return userResponse;
        }

        public IEnumerable<UserResponse> Execute(IEnumerable<User> users)
        {
            return users.Select(Execute);
        }
    }
}