using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericQuery;
using FoodManager.Model;

namespace FoodManager.Queries.Workers
{
    public interface IWorkerQuery : IQuery
    {
        void WithOnlyActivated(bool onlyActivated);
        void WithOnlyStatusActivated(bool onlyStatusActivated);
        void WithOnlyStatusDeactivated(bool onlyStatusDeactivated);
        void WithDepartment(int departmentId);
        void WithJob(int jobId);
        void WithDealer(int dealerId);
        IEnumerable<Worker> Execute();
    }
}