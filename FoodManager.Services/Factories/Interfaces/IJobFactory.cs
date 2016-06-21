using System.Collections.Generic;
using FoodManager.DTO.Message.Jobs;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IJobFactory
    {
        JobResponse Execute(Job job);
        IEnumerable<JobResponse> Execute(IEnumerable<Job> jobs);
    }
}