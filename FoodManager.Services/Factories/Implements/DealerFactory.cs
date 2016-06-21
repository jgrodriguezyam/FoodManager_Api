using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Dealers;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class DealerFactory : IDealerFactory
    {
        private readonly IDealerRepository _dealerRepository;

        public DealerFactory(IDealerRepository dealerRepository)
        {
            _dealerRepository = dealerRepository;
        }

        public DealerResponse Execute(Dealer dealer)
        {
            var dealerResponse = TypeAdapter.Adapt<DealerResponse>(dealer);
            dealerResponse.IsReference = _dealerRepository.IsReference(dealer.Id);
            return dealerResponse;
        }

        public IEnumerable<DealerResponse> Execute(IEnumerable<Dealer> dealers)
        {
            return dealers.Select(Execute);
        }
    }
}