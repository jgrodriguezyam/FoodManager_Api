using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Companies;
using FoodManager.DTO.Message.Regions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class CompanyFactory : ICompanyFactory
    {
        private readonly IRegionRepository _regionRepository;

        public CompanyFactory(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public CompanyResponse Execute(Company company)
        {
            var companyResponse = TypeAdapter.Adapt<CompanyResponse>(company);
            var region = _regionRepository.FindBy(company.RegionId);
            companyResponse.Region = TypeAdapter.Adapt<RegionResponse>(region);
            return companyResponse;
        }

        public IEnumerable<CompanyResponse> Execute(IEnumerable<Company> companies)
        {
            return companies.Select(Execute);
        }
    }
}