using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Regions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class RegionFactory : IRegionFactory
    {
        private readonly IRegionRepository _regionRepository;

        public RegionFactory(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public RegionResponse Execute(Region region)
        {
            var regionResponse = TypeAdapter.Adapt<RegionResponse>(region);
            regionResponse.IsReference = _regionRepository.IsReference(region.Id);
            return regionResponse;
        }

        public IEnumerable<RegionResponse> Execute(IEnumerable<Region> regions)
        {
            return regions.Select(Execute);
        }
    }
}