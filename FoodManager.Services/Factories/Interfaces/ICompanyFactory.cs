using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface ICompanyFactory
    {
        DTO.Company Execute(Company company);
    }
}