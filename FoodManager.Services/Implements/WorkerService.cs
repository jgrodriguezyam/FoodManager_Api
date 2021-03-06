using System.Collections.Generic;
using System.Linq;
using System.Net;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Workers;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Model;
using FoodManager.Model.IHmac;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.Workers;
using FoodManager.Services.Factories.Interfaces;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class WorkerService : IWorkerService
    {
        private readonly IWorkerQuery _workerQuery;
        private readonly IWorkerRepository _workerRepository;
        private readonly IWorkerValidator _workerValidator;
        private readonly IHmacHelper _hmacHelper;
        private readonly IWorkerFactory _workerFactory;

        public WorkerService(IWorkerQuery workerQuery, IWorkerRepository workerRepository, IWorkerValidator workerValidator, IHmacHelper hmacHelper, IWorkerFactory workerFactory)
        {
            _workerQuery = workerQuery;
            _workerRepository = workerRepository;
            _workerValidator = workerValidator;
            _hmacHelper = hmacHelper;
            _workerFactory = workerFactory;
        }

        public FindWorkersResponse Find(FindWorkersRequest request)
        {
            try
            {
                _workerQuery.WithOnlyActivated(true);
                _workerQuery.WithOnlyStatusActivated(request.OnlyStatusActivated);
                _workerQuery.WithOnlyStatusDeactivated(request.OnlyStatusDeactivated);
                _workerQuery.WithDepartment(request.DepartmentId);
                _workerQuery.WithJob(request.JobId);
                _workerQuery.WithRole(request.RoleId);
                _workerQuery.WithBranch(request.BranchId);
                _workerQuery.WithCode(request.Code);
                _workerQuery.WithEmail(request.Email);
                _workerQuery.WithBadge(request.Badge);
                _workerQuery.WithImss(request.Imss);
                _workerQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _workerQuery.TotalRecords();
                _workerQuery.Paginate(request.StartPage, request.EndPage);
                var workers = _workerQuery.Execute();

                return new FindWorkersResponse
                {
                    Workers = TypeAdapter.Adapt<List<WorkerResponse>>(workers),
                    TotalRecords = totalRecords
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public CreateResponse Create(WorkerRequest request)
        {
            try
            {
                var worker = TypeAdapter.Adapt<Worker>(request);
                _workerValidator.ValidateAndThrowException(worker, "Base");
                _workerRepository.Add(worker);
                return new CreateResponse(worker.Id);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Update(WorkerRequest request)
        {
            try
            {
                var currentWorker = _workerRepository.FindBy(request.Id);
                currentWorker.ThrowExceptionIfRecordIsNull();
                var workerToCopy = TypeAdapter.Adapt<Worker>(request);
                TypeAdapter.Adapt(workerToCopy, currentWorker);
                _workerValidator.ValidateAndThrowException(currentWorker, "Base");
                _workerRepository.Update(currentWorker);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public DTO.Worker Get(GetWorkerRequest request)
        {
            try
            {
                var worker = _workerRepository.FindBy(request.Id);
                worker.ThrowExceptionIfRecordIsNull();
                return _workerFactory.Execute(worker);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Delete(DeleteWorkerRequest request)
        {
            try
            {
                var worker = _workerRepository.FindBy(request.Id);
                worker.ThrowExceptionIfRecordIsNull();
                var isReference = _workerRepository.IsReference(request.Id);
                isReference.ThrowExceptionIfIsReference();
                _workerRepository.Remove(worker);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public LoginWorkerResponse Login(LoginWorkerRequest request)
        {
            try
            {
                var workerToUpdate = _workerRepository.FindBy(worker => worker.Badge == request.Badge).FirstOrDefault();
                workerToUpdate.ThrowExceptionIfIsNull(HttpStatusCode.Unauthorized, "Credenciales invalidas");
                workerToUpdate.Login();
                _hmacHelper.UpdateHmacOfWorker(workerToUpdate);

                return new LoginWorkerResponse
                {
                    WorkerId = workerToUpdate.Id,
                    PublicKey = workerToUpdate.PublicKey
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Logout(LogoutWorkerRequest request)
        {
            try
            {
                var worker = _workerRepository.FindBy(request.Id);
                worker.ThrowExceptionIfRecordIsNull();
                worker.Logout();
                _hmacHelper.UpdateHmacOfWorker(worker);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse ChangeStatus(ChangeStatusRequest request)
        {
            try
            {
                var worker = _workerRepository.FindBy(request.Id);
                worker.ThrowExceptionIfRecordIsNull();
                worker.Status.ThrowExceptionIfIsSameStatus(request.Status);
                worker.Status = request.Status;
                _workerRepository.Update(worker);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse IsReference(IsReferenceRequest request)
        {
            try
            {
                var isReference = _workerRepository.IsReference(request.Id);
                return new SuccessResponse { IsSuccess = isReference };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}