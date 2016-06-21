using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Departments;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class DepartmentFactory : IDepartmentFactory
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentFactory(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public DepartmentResponse Execute(Department department)
        {
            var departmentResponse = TypeAdapter.Adapt<DepartmentResponse>(department);
            departmentResponse.IsReference = _departmentRepository.IsReference(department.Id);
            return departmentResponse;
        }

        public IEnumerable<DepartmentResponse> Execute(IEnumerable<Department> departments)
        {
            return departments.Select(Execute);
        }
    }
}