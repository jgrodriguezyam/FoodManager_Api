using System.Linq;
using System.Net;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Enums;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Infrastructure.Objects;
using FoodManager.Infrastructure.Validators.Enums;
using FoodManager.Model;
using FoodManager.Model.Enums;
using FoodManager.Model.IRepositories;
using FoodManager.SoapService.Interfaces;

namespace FoodManager.SoapService.Implements
{
    public class WorkerSoapRepository : IWorkerSoapRepository
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IJobRepository _jobRepository;

        public WorkerSoapRepository(IWorkerRepository workerRepository, IBranchRepository branchRepository, IDepartmentRepository departmentRepository, IJobRepository jobRepository)
        {
            _workerRepository = workerRepository;
            _branchRepository = branchRepository;
            _departmentRepository = departmentRepository;
            _jobRepository = jobRepository;
        }

        public void GetByBadge(string badge)
        {
            var currentWorker = _workerRepository.FindBy(worker => worker.Badge == badge && worker.IsActive).FirstOrDefault();
            if (currentWorker.IsNull())
            {
                var workerMySpace = GetByBadgeInSoapService(badge);
                if (workerMySpace.IsNull())
                    ExceptionExtensions.ThrowCustomException(HttpStatusCode.NotFound, CodeValidator.NotFound.GetValue(), "No se encontro al colaborador");

                var workerModelImss = _workerRepository.FindBy(worker => worker.Imss == workerMySpace.Imss && worker.IsActive).FirstOrDefault();
                if (workerModelImss.IsNotNull())
                {
                    workerModelImss.Badge = workerMySpace.Badge;
                    _workerRepository.UpdateForSystem(workerModelImss);
                }
                else
                {
                    _workerRepository.AddForSystem(workerMySpace);
                }
            }
        }

        private Worker GetByBadgeInSoapService(string badge)
        {
            var workerClient = new BepensaMySpaceService.ServicioEjemploSoapClient();
            var workerMySpace = workerClient.wsgafetecomedor(badge);
            var dataRow = workerMySpace.Tables[0].Rows[0];
            if (dataRow.ItemArray.Count() > 1)
            {
                var code = dataRow["emp"].ToString();
                var firstName = dataRow["nombres"].ToString();
                var lastName = string.Format("{0} {1}", dataRow["paterno"], dataRow["materno"]);
                var email = dataRow["email"].ToString();
                var imss = dataRow["imss"].ToString();
                var gender = dataRow["sexo"].Equals("F") ? GenderType.Female.GetValue() : GenderType.Male.GetValue();

                var branchCode = dataRow["suc"].ToString();
                var branch = _branchRepository.FindBy(branchModel => branchModel.Code == branchCode && branchModel.IsActive).FirstOrDefault();
                if (branch.IsNull())
                    ExceptionExtensions.ThrowCustomException(HttpStatusCode.Conflict, CodeValidator.Conflict.GetValue(), "El Sucursal esta desactivada o no existe");
                var departmentName = dataRow["depto"].ToString();
                var department = _departmentRepository.FindBy(departmentModel => departmentModel.Name == departmentName && departmentModel.IsActive).FirstOrDefault();
                if (department.IsNull())
                    ExceptionExtensions.ThrowCustomException(HttpStatusCode.Conflict, CodeValidator.Conflict.GetValue(), "El Departamento esta desactivado o no existe");
                var jobName = dataRow["nompuesto"].ToString();
                var job = _jobRepository.FindBy(jobModel => jobModel.Name == jobName && jobModel.IsActive).FirstOrDefault();
                if (job.IsNull())
                    ExceptionExtensions.ThrowCustomException(HttpStatusCode.Conflict, CodeValidator.Conflict.GetValue(), "El Puesto esta desactivado o no existe");

                return new Worker
                {
                    Badge = badge,
                    Code = code,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Imss = imss,
                    Gender = gender,
                    BranchId = branch.Id,
                    DepartmentId = department.Id,
                    JobId = job.Id,
                    RoleId = GlobalConstants.WorkerRoleId
                };
            }
            return null;
        }
    }
}