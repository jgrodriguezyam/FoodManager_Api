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
        private readonly IDealerRepository _dealerRepository;

        public WorkerFactory(IDepartmentRepository departmentRepository, IJobRepository jobRepository, IDealerRepository dealerRepository)
        {
            _departmentRepository = departmentRepository;
            _jobRepository = jobRepository;
            _dealerRepository = dealerRepository;
        }

        public DTO.Worker Execute(Worker worker)
        {
            var workerDto = TypeAdapter.Adapt<DTO.Worker>(worker);
            var department = _departmentRepository.FindBy(worker.DepartmentId);
            workerDto.Department = TypeAdapter.Adapt<DTO.Department>(department);
            var job = _jobRepository.FindBy(worker.JobId);
            workerDto.Job = TypeAdapter.Adapt<DTO.Job>(job);
            var dealer = _dealerRepository.FindBy(worker.DealerId);
            workerDto.Dealer = TypeAdapter.Adapt<DTO.Dealer>(dealer);
            return workerDto;
        }
    }
}