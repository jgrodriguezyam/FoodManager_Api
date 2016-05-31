using FastMapper;
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

        public DTO.User Execute(User user)
        {
            var userDto = TypeAdapter.Adapt<DTO.User>(user);
            var dealerId = user.DealerId ?? 0;
            var dealer = _dealerRepository.FindBy(dealerId);
            if (dealer.IsNotNull())
                userDto.Dealer = TypeAdapter.Adapt<DTO.Dealer>(dealer);
            var role = _roleRepository.FindBy(user.RoleId);
            userDto.Role = TypeAdapter.Adapt<DTO.Role>(role);
            return userDto;
        }
    }
}