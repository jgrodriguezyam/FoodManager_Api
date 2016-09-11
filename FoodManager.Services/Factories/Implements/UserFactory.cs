using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Dealers;
using FoodManager.DTO.Message.Roles;
using FoodManager.DTO.Message.Users;
using FoodManager.Infrastructure.Integers;
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
            return AppendProperties(new[] { user }).FirstOrDefault();
        }

        public IEnumerable<UserResponse> Execute(IEnumerable<User> users)
        {
            return AppendProperties(users);
        }

        private IEnumerable<UserResponse> AppendProperties(IEnumerable<User> users)
        {
            var usersResponse = TypeAdapter.Adapt<List<UserResponse>>(users);
            var dealers = _dealerRepository.FindBy(dealer => dealer.IsActive);
            var roles = _roleRepository.FindBy(role => role.IsActive);

            usersResponse.ForEach(userResponse =>
            {
                var user = users.First(userModel => userModel.Id == userResponse.Id);
                var dealerId = user.DealerId ?? 0;
                if (dealerId.IsNotZero())
                {
                    var dealer = dealers.First(dealerModel => dealerModel.Id == user.DealerId);
                    userResponse.Dealer = TypeAdapter.Adapt<DealerResponse>(dealer);
                }
                var role = roles.First(roleModel => roleModel.Id == user.RoleId);
                userResponse.Role = TypeAdapter.Adapt<RoleResponse>(role);
            });

            return usersResponse;
        }
    }
}