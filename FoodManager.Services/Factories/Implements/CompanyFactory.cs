using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Companies;
using FoodManager.Infrastructure.Integers;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class CompanyFactory : ICompanyFactory
    {
        private readonly IBranchRepository _branchRepository;

        public CompanyFactory(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public CompanyResponse Execute(Company company)
        {
            return AppendProperties(new[] { company }).FirstOrDefault();
        }

        public IEnumerable<CompanyResponse> Execute(IEnumerable<Company> companies)
        {
            return AppendProperties(companies);
        }

        private IEnumerable<CompanyResponse> AppendProperties(IEnumerable<Company> companies)
        {
            var companiesResponse = TypeAdapter.Adapt<List<CompanyResponse>>(companies);
            var branches = _branchRepository.FindBy(branch => branch.IsActive);

            companiesResponse.ForEach(companyResponse =>
            {
                var company = companies.First(companyModel => companyModel.Id == companyResponse.Id);
                var amountOfReferences = branches.Count(branch => branch.CompanyId == company.Id);
                companyResponse.IsReference = amountOfReferences.IsNotZero();
            });

            return companiesResponse;
        }
    }
}