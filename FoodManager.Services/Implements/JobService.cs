using System.Collections.Generic;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Jobs;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.Jobs;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class JobService : IJobService
    {
        private readonly IJobQuery _jobQuery;
        private readonly IJobRepository _jobRepository;
        private readonly IJobValidator _jobValidator;

        public JobService(IJobQuery jobQuery, IJobRepository jobRepository, IJobValidator jobValidator)
        {
            _jobQuery = jobQuery;
            _jobRepository = jobRepository;
            _jobValidator = jobValidator;
        }

        public FindJobsResponse Find(FindJobsRequest request)
        {
            try
            {
                _jobQuery.WithOnlyActivated(true);
                _jobQuery.WithOnlyStatusActivated(request.OnlyStatusActivated);
                _jobQuery.WithOnlyStatusDeactivated(request.OnlyStatusDeactivated);
                _jobQuery.WithName(request.Name);
                _jobQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _jobQuery.TotalRecords();
                _jobQuery.Paginate(request.StartPage, request.EndPage);
                var jobs = _jobQuery.Execute();

                return new FindJobsResponse
                {
                    Jobs = TypeAdapter.Adapt<List<JobResponse>>(jobs),
                    TotalRecords = totalRecords
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public CreateResponse Create(JobRequest request)
        {
            try
            {
                var job = TypeAdapter.Adapt<Job>(request);
                _jobValidator.ValidateAndThrowException(job, "Base");
                _jobRepository.Add(job);
                return new CreateResponse(job.Id);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Update(JobRequest request)
        {
            try
            {
                var currentJob = _jobRepository.FindBy(request.Id);
                currentJob.ThrowExceptionIfRecordIsNull();
                var jobToCopy = TypeAdapter.Adapt<Job>(request);
                TypeAdapter.Adapt(jobToCopy, currentJob);
                _jobValidator.ValidateAndThrowException(currentJob, "Base");
                _jobRepository.Update(currentJob);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public DTO.Job Get(GetJobRequest request)
        {
            try
            {
                var job = _jobRepository.FindBy(request.Id);
                job.ThrowExceptionIfRecordIsNull();
                return TypeAdapter.Adapt<DTO.Job>(job);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Delete(DeleteJobRequest request)
        {
            try
            {
                var job = _jobRepository.FindBy(request.Id);
                job.ThrowExceptionIfRecordIsNull();
                var isReference = _jobRepository.IsReference(request.Id);
                isReference.ThrowExceptionIfIsReference();
                _jobRepository.Remove(job);
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
                var job = _jobRepository.FindBy(request.Id);
                job.ThrowExceptionIfRecordIsNull();
                job.Status.ThrowExceptionIfIsSameStatus(request.Status);
                job.Status = request.Status;
                _jobRepository.Update(job);
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
                var isReference = _jobRepository.IsReference(request.Id);
                return new SuccessResponse { IsSuccess = isReference };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}