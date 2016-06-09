using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Dealers;
using FoodManager.DTO.Message.Menus;
using FoodManager.DTO.Message.Saucers;
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

        public MenuResponse Execute(Menu menu)
        {
            var menuResponse = TypeAdapter.Adapt<MenuResponse>(menu);
            var dealer = _dealerRepository.FindBy(menu.DealerId);
            menuResponse.Dealer = TypeAdapter.Adapt<DealerResponse>(dealer);
            var saucer = _saucerRepository.FindBy(menu.SaucerId);
            menuResponse.Saucer = TypeAdapter.Adapt<SaucerResponse>(saucer);
            return menuResponse;
        }

        public IEnumerable<MenuResponse> Execute(IEnumerable<Menu> menus)
        {
            return menus.Select(Execute);
        }
    }
}