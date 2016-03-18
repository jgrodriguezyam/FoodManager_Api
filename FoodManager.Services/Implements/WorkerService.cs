using System.Collections.Generic;
using System.Net;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Workers;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.Workers;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class WorkerService : IWorkerService
    {
        private readonly IWorkerQuery _workerQuery;
        private readonly IWorkerRepository _workerRepository;
        private readonly IWorkerValidator _workerValidator;

        public WorkerService(IWorkerQuery workerQuery, IWorkerRepository workerRepository, IWorkerValidator workerValidator)
        {
            _workerQuery = workerQuery;
            _workerRepository = workerRepository;
            _workerValidator = workerValidator;
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
                _workerQuery.WithDealer(request.DealerId);
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
                currentWorker.ThrowExceptionIfIsNull("Trabajador no encontrado");
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
                worker.ThrowExceptionIfIsNull("Trabajador no encontrado");
                return TypeAdapter.Adapt<DTO.Worker>(worker);
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
                worker.ThrowExceptionIfIsNull("Trabajador no encontrado");
                _workerRepository.Remove(worker);
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
                worker.ThrowExceptionIfIsNull("Trabajador no encontrado");
                if (worker.Status.Equals(request.Status))
                    ExceptionExtensions.ThrowStatusException(HttpStatusCode.Accepted, request.Status);
                worker.Status = request.Status;
                _workerRepository.Update(worker);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}