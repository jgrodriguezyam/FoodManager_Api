using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Jobs;
using FoodManager.Infrastructure.Integers;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class JobFactory : IJobFactory
    {
        private readonly IWorkerRepository _workerRepository;

        public JobFactory(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }

        public JobResponse Execute(Job job)
        {
            return AppendProperties(new[] { job }).FirstOrDefault();
        }

        public IEnumerable<JobResponse> Execute(IEnumerable<Job> jobs)
        {
            return AppendProperties(jobs);
        }

        private IEnumerable<JobResponse> AppendProperties(IEnumerable<Job> jobs)
        {
            var jobsResponse = TypeAdapter.Adapt<List<JobResponse>>(jobs);
            var workers = _workerRepository.FindBy(worker => worker.IsActive);

            jobsResponse.ForEach(jobResponse =>
            {
                var job = jobs.First(jobModel => jobModel.Id == jobResponse.Id);
                var amountOfReferences = workers.Count(worker => worker.JobId == job.Id);
                jobResponse.IsReference = amountOfReferences.IsNotZero();
            });

            return jobsResponse;
        }
    }
}