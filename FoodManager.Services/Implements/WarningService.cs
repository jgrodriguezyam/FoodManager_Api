using System.Collections.Generic;
using System.Net;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Warnings;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.Warnings;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class WarningService : IWarningService
    {
        private readonly IWarningQuery _warningQuery;
        private readonly IWarningRepository _warningRepository;
        private readonly IWarningValidator _warningValidator;

        public WarningService(IWarningQuery warningQuery, IWarningRepository warningRepository, IWarningValidator warningValidator)
        {
            _warningQuery = warningQuery;
            _warningRepository = warningRepository;
            _warningValidator = warningValidator;
        }

        public FindWarningsResponse Find(FindWarningsRequest request)
        {
            try
            {
                _warningQuery.WithOnlyActivated(true);
                _warningQuery.WithOnlyStatusActivated(request.OnlyStatusActivated);
                _warningQuery.WithOnlyStatusDeactivated(request.OnlyStatusDeactivated);
                _warningQuery.WithDisease(request.DiseaseId);
                _warningQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _warningQuery.TotalRecords();
                _warningQuery.Paginate(request.StartPage, request.EndPage);
                var warnings = _warningQuery.Execute();

                return new FindWarningsResponse
                {
                    Warnings = TypeAdapter.Adapt<List<WarningResponse>>(warnings),
                    TotalRecords = totalRecords
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public CreateResponse Create(WarningRequest request)
        {
            try
            {
                var warning = TypeAdapter.Adapt<Warning>(request);
                _warningValidator.ValidateAndThrowException(warning, "Base");
                _warningRepository.Add(warning);
                return new CreateResponse(warning.Id);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Update(WarningRequest request)
        {
            try
            {
                var currentWarning = _warningRepository.FindBy(request.Id);
                currentWarning.ThrowExceptionIfIsNull("Advertencia no encontrada");
                var warningToCopy = TypeAdapter.Adapt<Warning>(request);
                TypeAdapter.Adapt(warningToCopy, currentWarning);
                _warningValidator.ValidateAndThrowException(currentWarning, "Base");
                _warningRepository.Update(currentWarning);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public DTO.Warning Get(GetWarningRequest request)
        {
            try
            {
                var warning = _warningRepository.FindBy(request.Id);
                warning.ThrowExceptionIfIsNull("Advertencia no encontrada");
                return TypeAdapter.Adapt<DTO.Warning>(warning);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Delete(DeleteWarningRequest request)
        {
            try
            {
                var warning = _warningRepository.FindBy(request.Id);
                warning.ThrowExceptionIfIsNull("Advertencia no encontrada");
                _warningRepository.Remove(warning);
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
                var warning = _warningRepository.FindBy(request.Id);
                warning.ThrowExceptionIfIsNull("Advertencia no encontrada");
                if (warning.Status.Equals(request.Status))
                    ExceptionExtensions.ThrowStatusException(HttpStatusCode.Accepted, request.Status);
                warning.Status = request.Status;
                _warningRepository.Update(warning);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}