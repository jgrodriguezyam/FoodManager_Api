using System.Collections.Generic;
using FoodManager.DTO.Message.Workers;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IWorkerFactory
    {
        WorkerResponse Execute(Worker worker);
        IEnumerable<WorkerResponse> Execute(IEnumerable<Worker> workers);
        IEnumerable<WorkerTopReportResponse> Execute(WorkerReportRequest workerReportRequest);
    }
}