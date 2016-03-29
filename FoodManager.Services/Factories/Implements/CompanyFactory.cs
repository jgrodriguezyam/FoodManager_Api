using FastMapper;
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

        public DTO.Company Execute(Company company)
        {
            var companyDto = TypeAdapter.Adapt<DTO.Company>(company);
            var region = _regionRepository.FindBy(company.RegionId);
            companyDto.Region = TypeAdapter.Adapt<DTO.Region>(region);
            return companyDto;
        }
    }
}