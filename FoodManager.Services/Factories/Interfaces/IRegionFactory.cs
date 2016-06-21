using System.Collections.Generic;
using FoodManager.DTO.Message.Regions;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IRegionFactory
    {
        RegionResponse Execute(Region region);
        IEnumerable<RegionResponse> Execute(IEnumerable<Region> regions);
    }
}