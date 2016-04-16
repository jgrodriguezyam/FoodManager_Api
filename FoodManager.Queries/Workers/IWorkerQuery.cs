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
        void WithRole(int roleId);
        void WithCode(string code);
        void WithEmail(string email);
        void WithBadge(string badge);
        void WithImss(string imss); 
        IEnumerable<Worker> Execute();
    }
}