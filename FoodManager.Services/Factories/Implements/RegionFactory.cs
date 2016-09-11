using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Regions;
using FoodManager.Infrastructure.Integers;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class RegionFactory : IRegionFactory
    {
        private readonly IBranchRepository _branchRepository;

        public RegionFactory(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public RegionResponse Execute(Region region)
        {
            return AppendProperties(new[] { region }).FirstOrDefault();
        }

        public IEnumerable<RegionResponse> Execute(IEnumerable<Region> regions)
        {
            return AppendProperties(regions);
        }

        private IEnumerable<RegionResponse> AppendProperties(IEnumerable<Region> regions)
        {
            var regionsResponse = TypeAdapter.Adapt<List<RegionResponse>>(regions);
            var branches = _branchRepository.FindBy(branch => branch.IsActive);

            regionsResponse.ForEach(regionResponse =>
            {
                var region = regions.First(regionModel => regionModel.Id == regionResponse.Id);
                var amountOfReferences = branches.Count(branch => branch.RegionId == region.Id);
                regionResponse.IsReference = amountOfReferences.IsNotZero();
            });

            return regionsResponse;
        }
    }
}