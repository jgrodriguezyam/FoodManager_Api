using FoodManager.Infrastructure.IGenericRepositories;

namespace FoodManager.Model.IRepositories
{
    public interface ICompanyRepository : IRepository<Company>
    {
        bool IsReference(int companyId);
    }
}