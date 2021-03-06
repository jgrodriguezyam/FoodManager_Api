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

        public UserFactory(IDealerRepository dealerRepository)
        {
            _dealerRepository = dealerRepository;
        }

        public DTO.User Execute(User user)
        {
            var userDto = TypeAdapter.Adapt<DTO.User>(user);
            var dealerId = user.DealerId ?? 0;
            var dealer = _dealerRepository.FindBy(dealerId);
            if (dealer.IsNotNull())
                userDto.Dealer = TypeAdapter.Adapt<DTO.Dealer>(dealer);
            return userDto;
        }
    }
}