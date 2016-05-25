using FastMapper;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class BranchFactory : IBranchFactory
    {
        private readonly ICompanyRepository _companyRepository;

        public BranchFactory(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public DTO.Branch Execute(Branch branch)
        {
            var branchDto = TypeAdapter.Adapt<DTO.Branch>(branch);
            var company = _companyRepository.FindBy(branch.CompanyId);
            branchDto.Company = TypeAdapter.Adapt<DTO.Company>(company);
            return branchDto;
        }
    }
}