using System.Collections.Generic;
using FoodManager.DTO.Message.Companies;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface ICompanyFactory
    {
        CompanyResponse Execute(Company company);
        IEnumerable<CompanyResponse> Execute(IEnumerable<Company> companies);
    }
}