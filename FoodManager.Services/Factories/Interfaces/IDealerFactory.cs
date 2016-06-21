using System.Collections.Generic;
using FoodManager.DTO.Message.Dealers;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IDealerFactory
    {
        DealerResponse Execute(Dealer dealer);
        IEnumerable<DealerResponse> Execute(IEnumerable<Dealer> dealers);
    }
}