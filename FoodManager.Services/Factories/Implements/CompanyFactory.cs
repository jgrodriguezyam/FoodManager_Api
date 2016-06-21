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
        private readonly ICompanyRepository _companyRepository;

        public CompanyFactory(IRegionRepository regionRepository, ICompanyRepository companyRepository)
        {
            _regionRepository = regionRepository;
            _companyRepository = companyRepository;
        }

        public CompanyResponse Execute(Company company)
        {
            var companyResponse = TypeAdapter.Adapt<CompanyResponse>(company);
            var region = _regionRepository.FindBy(company.RegionId);
            companyResponse.Region = TypeAdapter.Adapt<RegionResponse>(region);
            companyResponse.IsReference = _companyRepository.IsReference(company.Id);
            return companyResponse;
        }

        public IEnumerable<CompanyResponse> Execute(IEnumerable<Company> companies)
        {
            return companies.Select(Execute);
        }
    }
}