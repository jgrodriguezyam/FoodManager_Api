using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Branches;
using FoodManager.DTO.Message.Departments;
using FoodManager.DTO.Message.Jobs;
using FoodManager.DTO.Message.Roles;
using FoodManager.DTO.Message.Workers;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class WorkerFactory : IWorkerFactory
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IWorkerRepository _workerRepository;

        public WorkerFactory(IDepartmentRepository departmentRepository, IJobRepository jobRepository, IRoleRepository roleRepository, IBranchRepository branchRepository, IWorkerRepository workerRepository)
        {
            _departmentRepository = departmentRepository;
            _jobRepository = jobRepository;
            _roleRepository = roleRepository;
            _branchRepository = branchRepository;
            _workerRepository = workerRepository;
        }

        public WorkerResponse Execute(Worker worker)
        {
            var workerResponse = TypeAdapter.Adapt<WorkerResponse>(worker);
            var department = _departmentRepository.FindBy(worker.DepartmentId);
            workerResponse.Department = TypeAdapter.Adapt<DepartmentResponse>(department);
            var job = _jobRepository.FindBy(worker.JobId);
            workerResponse.Job = TypeAdapter.Adapt<JobResponse>(job);
            var role = _roleRepository.FindBy(worker.RoleId);
            workerResponse.Role = TypeAdapter.Adapt<RoleResponse>(role);
            var branch = _branchRepository.FindBy(worker.BranchId);
            workerResponse.Branch = TypeAdapter.Adapt<BranchResponse>(branch);
            workerResponse.IsReference = _workerRepository.IsReference(worker.Id);
            return workerResponse;
        }

        public IEnumerable<WorkerResponse> Execute(IEnumerable<Worker> workers)
        {
            return workers.Select(Execute);
        }
    }
}