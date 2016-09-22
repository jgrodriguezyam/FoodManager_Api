using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Branches;
using FoodManager.DTO.Message.Departments;
using FoodManager.DTO.Message.Jobs;
using FoodManager.DTO.Message.Roles;
using FoodManager.DTO.Message.Workers;
using FoodManager.Infrastructure.Integers;
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
        private readonly IReservationRepository _reservationRepository;
        private readonly IWorkerRepository _workerRepository;
        private int _workerTop = 10;

        public WorkerFactory(IDepartmentRepository departmentRepository, IJobRepository jobRepository, IRoleRepository roleRepository, IBranchRepository branchRepository, IReservationRepository reservationRepository, IWorkerRepository workerRepository)
        {
            _departmentRepository = departmentRepository;
            _jobRepository = jobRepository;
            _roleRepository = roleRepository;
            _branchRepository = branchRepository;
            _reservationRepository = reservationRepository;
            _workerRepository = workerRepository;
        }

        public WorkerResponse Execute(Worker worker)
        {
            return AppendProperties(new[] { worker }).FirstOrDefault();
        }

        public IEnumerable<WorkerResponse> Execute(IEnumerable<Worker> workers)
        {
            return AppendProperties(workers);
        }

        private IEnumerable<WorkerResponse> AppendProperties(IEnumerable<Worker> workers)
        {
            var workersResponse = TypeAdapter.Adapt<List<WorkerResponse>>(workers);
            var departments = _departmentRepository.FindBy(department => department.IsActive);
            var jobs = _jobRepository.FindBy(job => job.IsActive);
            var roles = _roleRepository.FindBy(role => role.IsActive);
            var branches = _branchRepository.FindBy(branch => branch.IsActive);
            var reservations = _reservationRepository.FindBy(reservation => reservation.IsActive);

            workersResponse.ForEach(workerResponse =>
            {
                var worker = workers.First(workerModel => workerModel.Id == workerResponse.Id);
                var department = departments.First(departmentModel => departmentModel.Id == worker.DepartmentId);
                workerResponse.Department = TypeAdapter.Adapt<DepartmentResponse>(department);
                var job = jobs.First(jobModel => jobModel.Id == worker.JobId);
                workerResponse.Job = TypeAdapter.Adapt<JobResponse>(job);
                var role = roles.First(roleModel => roleModel.Id == worker.RoleId);
                workerResponse.Role = TypeAdapter.Adapt<RoleResponse>(role);
                var branch = branches.First(branchModel => branchModel.Id == worker.BranchId);
                workerResponse.Branch = TypeAdapter.Adapt<BranchResponse>(branch);
                var amountOfReferences = reservations.Count(reservation => reservation.WorkerId == worker.Id);
                workerResponse.IsReference = amountOfReferences.IsNotZero();
            });

            return workersResponse;
        }

        public IEnumerable<WorkerTopReportResponse> Execute(WorkerReportRequest workerReportRequest)
        {
            var workers = _workerRepository.FindBy(worker => worker.IsActive);
            var reservations = _reservationRepository.FindBy(reservation => reservation.IsActive);
            var workersGroup = reservations.GroupBy(reservation => reservation.WorkerId);
            var workersTop = workersGroup.Select(workerGroup => new WorkerTopReportResponse
                            {
                                WorkerId = workerGroup.Key,
                                ReservationCount = reservations.Where(reservation => reservation.WorkerId == workerGroup.Key).GroupBy(reservation => reservation.Date).Count()
                            })
                            .OrderByDescending(workerTop => workerTop.ReservationCount).Take(_workerTop).ToList();

            workersTop.ForEach(workerTop =>
            {
                var worker = workers.First(currentWorker => currentWorker.Id == workerTop.WorkerId);
                workerTop.FirstName = worker.FirstName;
                workerTop.LastName = worker.LastName;
            });

            return workersTop;
        }
    }
}