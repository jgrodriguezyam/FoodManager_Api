using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Branches;
using FoodManager.DTO.Message.Companies;
using FoodManager.DTO.Message.Regions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class BranchFactory : IBranchFactory
    {
        private readonly IRegionRepository _regionRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IBranchRepository _branchRepository;

        public BranchFactory(IRegionRepository regionRepository, ICompanyRepository companyRepository, IBranchRepository branchRepository)
        {
            _regionRepository = regionRepository;
            _companyRepository = companyRepository;
            _branchRepository = branchRepository;
        }

        public BranchResponse Execute(Branch branch)
        {
            var branchResponse = TypeAdapter.Adapt<BranchResponse>(branch);
            var region = _regionRepository.FindBy(branch.RegionId);
            branchResponse.Region = TypeAdapter.Adapt<RegionResponse>(region);
            var company = _companyRepository.FindBy(branch.CompanyId);
            branchResponse.Company = TypeAdapter.Adapt<CompanyResponse>(company);
            branchResponse.IsReference = _branchRepository.IsReference(branch.Id);
            return branchResponse;
        }

        public IEnumerable<BranchResponse> Execute(IEnumerable<Branch> branches)
        {
            return branches.Select(Execute);
        }
    }
}