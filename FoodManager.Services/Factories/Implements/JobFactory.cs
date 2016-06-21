using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Jobs;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class JobFactory : IJobFactory
    {
        private readonly IJobRepository _jobRepository;

        public JobFactory(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public JobResponse Execute(Job job)
        {
            var jobResponse = TypeAdapter.Adapt<JobResponse>(job);
            jobResponse.IsReference = _jobRepository.IsReference(job.Id);
            return jobResponse;
        }

        public IEnumerable<JobResponse> Execute(IEnumerable<Job> jobs)
        {
            return jobs.Select(Execute);
        }
    }
}