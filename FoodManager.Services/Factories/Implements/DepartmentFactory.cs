using FastMapper;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class DepartmentFactory : IDepartmentFactory
    {
        private readonly IBranchRepository _branchRepository;

        public DepartmentFactory(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public DTO.Department Execute(Department department)
        {
            var departmentDto = TypeAdapter.Adapt<DTO.Department>(department);
            var branch = _branchRepository.FindBy(department.BranchId);
            departmentDto.Branch = TypeAdapter.Adapt<DTO.Branch>(branch);
            return departmentDto;
        }
    }
}