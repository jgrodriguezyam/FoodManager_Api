using System.Collections.Generic;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Queries;
using FoodManager.Infrastructure.Strings;
using FoodManager.Model;
using FoodManager.OrmLite.DataBase;
using FoodManager.OrmLite.Utils;
using ServiceStack.OrmLite.SqlServer;

namespace FoodManager.Queries.Workers
{
    public class WorkerQuery : IWorkerQuery
    {
        private readonly SqlServerExpressionVisitor<Worker> _query;
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;

        public WorkerQuery(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _query = new SqlServerExpressionVisitor<Worker>();
        }

        public void WithOnlyActivated(bool onlyActivated)
        {
            if (onlyActivated)
                _query.Where(worker => worker.IsActive);
        }

        public void WithOnlyStatusActivated(bool onlyStatusActivated)
        {
            if (onlyStatusActivated)
                _query.Where(worker => worker.Status);
        }

        public void WithOnlyStatusDeactivated(bool onlyStatusDeactivated)
        {
            if (onlyStatusDeactivated)
                _query.Where(worker => worker.Status == GlobalConstants.StatusDeactivated);
        }

        public void WithDepartment(int departmentId)
        {
            if (departmentId.IsNotZero())
                _query.Where(worker => worker.DepartmentId == departmentId);
        }

        public void WithJob(int jobId)
        {
            if (jobId.IsNotZero())
                _query.Where(worker => worker.JobId == jobId);
        }

        public void WithRole(int roleId)
        {
            if (roleId.IsNotZero())
                _query.Where(worker => worker.RoleId == roleId);
        }

        public void WithBranch(int branchId)
        {
            if (branchId.IsNotZero())
                _query.Where(worker => worker.BranchId == branchId);
        }

        public void WithCode(string code)
        {
            if (code.IsNotNullOrEmpty())
                _query.Where(worker => worker.Code == code);
        }

        public void WithEmail(string email)
        {
            if (email.IsNotNullOrEmpty())
                _query.Where(worker => worker.Email == email);
        }

        public void WithBadge(string badge)
        {
            if (badge.IsNotNullOrEmpty())
                _query.Where(worker => worker.Badge == badge);
        }

        public void WithImss(string imss)
        {
            if (imss.IsNotNullOrEmpty())
                _query.Where(worker => worker.Imss == imss);
        }

        public void Sort(string sort, string sortBy)
        {
            sort = sort.SortResolver();
            var property = sortBy.SortByPropertyResolver<Worker>();

            if (sort.Equals(QueryConstants.OrderByDescending))
                _query.OrderByDescending(property);

            if (sort.Equals(QueryConstants.OrderByAscending))
                _query.OrderBy(property);
        }

        public void Paginate(int startPage, int endPage)
        {
            if (startPage.IsNotZero() && endPage.IsNotZero())
                _query.Limit(startPage.ConvertSkip(), startPage.ConvertRows(endPage));
        }

        public int TotalRecords()
        {
            return _dataBaseSqlServerOrmLite.Count(_query);
        }

        public IEnumerable<Worker> Execute()
        {
            return _dataBaseSqlServerOrmLite.FindExpressionVisitor(_query);
        }
    }
}