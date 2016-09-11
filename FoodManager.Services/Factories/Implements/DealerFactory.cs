using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Dealers;
using FoodManager.Infrastructure.Integers;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class DealerFactory : IDealerFactory
    {
        private readonly IBranchDealerRepository _branchDealerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IReservationRepository _reservationRepository;

        public DealerFactory(IBranchDealerRepository branchDealerRepository, IUserRepository userRepository, IMenuRepository menuRepository, IReservationRepository reservationRepository)
        {
            _branchDealerRepository = branchDealerRepository;
            _userRepository = userRepository;
            _menuRepository = menuRepository;
            _reservationRepository = reservationRepository;
        }

        public DealerResponse Execute(Dealer dealer)
        {
            return AppendProperties(new[] { dealer }).FirstOrDefault();
        }

        public IEnumerable<DealerResponse> Execute(IEnumerable<Dealer> dealers)
        {
            return AppendProperties(dealers);
        }

        private IEnumerable<DealerResponse> AppendProperties(IEnumerable<Dealer> dealers)
        {
            var dealersResponse = TypeAdapter.Adapt<List<DealerResponse>>(dealers);
            var branchDealers = _branchDealerRepository.FindAll();
            var users = _userRepository.FindBy(user => user.IsActive);
            var menus = _menuRepository.FindBy(menu => menu.IsActive);
            var reservations = _reservationRepository.FindBy(reservation => reservation.IsActive);

            dealersResponse.ForEach(dealerResponse =>
            {
                var dealer = dealers.First(dealerModel => dealerModel.Id == dealerResponse.Id);
                var amountOfReferences = branchDealers.Count(branchDealer => branchDealer.DealerId == dealer.Id);
                amountOfReferences += users.Count(user => user.DealerId == dealer.Id);
                amountOfReferences += menus.Count(menu => menu.DealerId == dealer.Id);
                amountOfReferences += reservations.Count(reservation => reservation.DealerId == dealer.Id);
                dealerResponse.IsReference = amountOfReferences.IsNotZero();
            });

            return dealersResponse;
        }
    }
}