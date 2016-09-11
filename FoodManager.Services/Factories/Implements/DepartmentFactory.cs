using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Departments;
using FoodManager.Infrastructure.Integers;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class DepartmentFactory : IDepartmentFactory
    {
        private readonly IWorkerRepository _workerRepository;

        public DepartmentFactory(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }

        public DepartmentResponse Execute(Department department)
        {
            return AppendProperties(new[] { department }).FirstOrDefault();
        }

        public IEnumerable<DepartmentResponse> Execute(IEnumerable<Department> departments)
        {
            return AppendProperties(departments);
        }

        private IEnumerable<DepartmentResponse> AppendProperties(IEnumerable<Department> departments)
        {
            var departmentsResponse = TypeAdapter.Adapt<List<DepartmentResponse>>(departments);
            var workers = _workerRepository.FindBy(worker => worker.IsActive);

            departmentsResponse.ForEach(departmentResponse =>
            {
                var department = departments.First(departmentModel => departmentModel.Id == departmentResponse.Id);
                var amountOfReferences = workers.Count(worker => worker.DepartmentId == department.Id);
                departmentResponse.IsReference = amountOfReferences.IsNotZero();
            });

            return departmentsResponse;
        }
    }
}