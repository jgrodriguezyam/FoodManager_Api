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
        void WithRole(int roleId);
        void WithBranch(int branchId);
        void WithCode(string code);
        void WithEmail(string email);
        void WithBadge(string badge);
        void WithImss(string imss);
        void WithOnlyBelongUser(bool onlyBelongUser);
        void WithCompany(int companyId);
        IEnumerable<Worker> Execute();
    }
}