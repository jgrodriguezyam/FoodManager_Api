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
            return AppendProperties(new[] { menu }).FirstOrDefault();
        }

        public IEnumerable<MenuResponse> Execute(IEnumerable<Menu> menus)
        {
            return AppendProperties(menus);
        }

        private IEnumerable<MenuResponse> AppendProperties(IEnumerable<Menu> menus)
        {
            var menusResponse = TypeAdapter.Adapt<List<MenuResponse>>(menus);
            var dealers = _dealerRepository.FindBy(dealer => dealer.IsActive);
            var saucers = _saucerRepository.FindBy(saucer => saucer.IsActive);

            menusResponse.ForEach(menuResponse =>
            {
                var menu = menus.First(menuModel => menuModel.Id == menuResponse.Id);
                var dealer = dealers.First(dealerModel => dealerModel.Id == menu.DealerId);
                menuResponse.Dealer = TypeAdapter.Adapt<DealerResponse>(dealer);
                var saucer = saucers.First(saucerModel => saucerModel.Id == menu.SaucerId);
                menuResponse.Saucer = TypeAdapter.Adapt<SaucerResponse>(saucer);
            });

            return menusResponse;
        }
    }
}