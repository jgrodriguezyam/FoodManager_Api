using System.Collections.Generic;
using FoodManager.DTO.Message.Departments;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IDepartmentFactory
    {
        DepartmentResponse Execute(Department department);
        IEnumerable<DepartmentResponse> Execute(IEnumerable<Department> departments);
    }
}