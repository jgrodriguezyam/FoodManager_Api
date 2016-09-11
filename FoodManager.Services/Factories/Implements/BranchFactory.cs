using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Branches;
using FoodManager.DTO.Message.Companies;
using FoodManager.DTO.Message.Regions;
using FoodManager.Infrastructure.Integers;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class BranchFactory : IBranchFactory
    {
        private readonly IRegionRepository _regionRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IWorkerRepository _workerRepository;

        public BranchFactory(IRegionRepository regionRepository, ICompanyRepository companyRepository, IWorkerRepository workerRepository)
        {
            _regionRepository = regionRepository;
            _companyRepository = companyRepository;
            _workerRepository = workerRepository;
        }

        public BranchResponse Execute(Branch branch)
        {
            return AppendProperties(new[] { branch }).FirstOrDefault();
        }

        public IEnumerable<BranchResponse> Execute(IEnumerable<Branch> branches)
        {
            return AppendProperties(branches);
        }

        private IEnumerable<BranchResponse> AppendProperties(IEnumerable<Branch> branches)
        {
            var branchesResponse = TypeAdapter.Adapt<List<BranchResponse>>(branches);
            var regions = _regionRepository.FindBy(region => region.IsActive);
            var companies = _companyRepository.FindBy(company => company.IsActive);
            var workers = _workerRepository.FindBy(worker => worker.IsActive);

            branchesResponse.ForEach(branchResponse =>
            {
                var branch = branches.First(branchModel => branchModel.Id == branchResponse.Id);
                var region = regions.First(regionModel => regionModel.Id == branch.RegionId);
                branchResponse.Region = TypeAdapter.Adapt<RegionResponse>(region);
                var company = companies.First(companyModel => companyModel.Id == branch.CompanyId);
                branchResponse.Company = TypeAdapter.Adapt<CompanyResponse>(company);
                var amountOfReferences = workers.Count(worker => worker.BranchId == branch.Id);
                branchResponse.IsReference = amountOfReferences.IsNotZero();
            });

            return branchesResponse;
        }
    }
}