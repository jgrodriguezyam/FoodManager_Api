using FastMapper;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class MenuFactory : IMenuFactory
    {
        private readonly IDealerRepository _dealerRepository;
        private readonly ISaucerRepository _saucerRepository;

        public MenuFactory(IDealerRepository dealerRepository, ISaucerRepository saucerRepository)
        {
            _dealerRepository = dealerRepository;
            _saucerRepository = saucerRepository;
        }

        public DTO.Menu Execute(Menu menu)
        {
            var menuDto = TypeAdapter.Adapt<DTO.Menu>(menu);
            var dealer = _dealerRepository.FindBy(menu.DealerId);
            menuDto.Dealer = TypeAdapter.Adapt<DTO.Dealer>(dealer);
            var saucer = _saucerRepository.FindBy(menu.SaucerId);
            menuDto.Saucer = TypeAdapter.Adapt<DTO.Saucer>(saucer);
            return menuDto;
        }
    }
}