using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Branches;
using FoodManager.DTO.Message.Companies;
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

        public BranchResponse Execute(Branch branch)
        {
            var branchResponse = TypeAdapter.Adapt<BranchResponse>(branch);
            var company = _companyRepository.FindBy(branch.CompanyId);
            branchResponse.Company = TypeAdapter.Adapt<CompanyResponse>(company);
            return branchResponse;
        }

        public IEnumerable<BranchResponse> Execute(IEnumerable<Branch> branches)
        {
            return branches.Select(Execute);
        }
    }
}