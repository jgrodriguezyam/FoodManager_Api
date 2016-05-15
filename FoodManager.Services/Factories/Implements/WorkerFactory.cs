using FastMapper;
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

        public WorkerFactory(IDepartmentRepository departmentRepository, IJobRepository jobRepository, IRoleRepository roleRepository, IBranchRepository branchRepository)
        {
            _departmentRepository = departmentRepository;
            _jobRepository = jobRepository;
            _roleRepository = roleRepository;
            _branchRepository = branchRepository;
        }

        public DTO.Worker Execute(Worker worker)
        {
            var workerDto = TypeAdapter.Adapt<DTO.Worker>(worker);
            var department = _departmentRepository.FindBy(worker.DepartmentId);
            workerDto.Department = TypeAdapter.Adapt<DTO.Department>(department);
            var job = _jobRepository.FindBy(worker.JobId);
            workerDto.Job = TypeAdapter.Adapt<DTO.Job>(job);
            var role = _roleRepository.FindBy(worker.RoleId);
            workerDto.Role = TypeAdapter.Adapt<DTO.Role>(role);
            var branch = _branchRepository.FindBy(worker.BranchId);
            workerDto.Branch = TypeAdapter.Adapt<DTO.Branch>(branch);
            return workerDto;
        }
    }
}