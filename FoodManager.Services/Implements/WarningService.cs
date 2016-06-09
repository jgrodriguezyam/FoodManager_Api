using System.Linq;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Warnings;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.Warnings;
using FoodManager.Services.Factories.Interfaces;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class WarningService : IWarningService
    {
        private readonly IWarningQuery _warningQuery;
        private readonly IWarningRepository _warningRepository;
        private readonly IWarningValidator _warningValidator;
        private readonly IWarningFactory _warningFactory;

        public WarningService(IWarningQuery warningQuery, IWarningRepository warningRepository, IWarningValidator warningValidator, IWarningFactory warningFactory)
        {
            _warningQuery = warningQuery;
            _warningRepository = warningRepository;
            _warningValidator = warningValidator;
            _warningFactory = warningFactory;
        }

        public FindWarningsResponse Find(FindWarningsRequest request)
        {
            try
            {
                _warningQuery.WithOnlyActivated(true);
                _warningQuery.WithOnlyStatusActivated(request.OnlyStatusActivated);
                _warningQuery.WithOnlyStatusDeactivated(request.OnlyStatusDeactivated);
                _warningQuery.WithDisease(request.DiseaseId);
                _warningQuery.WithName(request.Name);
                _warningQuery.WithCode(request.Code);
                _warningQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _warningQuery.TotalRecords();
                _warningQuery.Paginate(request.StartPage, request.EndPage);
                var warnings = _warningQuery.Execute();

                return new FindWarningsResponse
                {
                    Warnings = _warningFactory.Execute(warnings).ToList(),
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
                _warningValidator.ValidateAndThrowException(warning, "Base,Create");
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
                currentWarning.ThrowExceptionIfRecordIsNull();
                var warningToCopy = TypeAdapter.Adapt<Warning>(request);
                TypeAdapter.Adapt(warningToCopy, currentWarning);
                _warningValidator.ValidateAndThrowException(currentWarning, "Base,Update");
                _warningRepository.Update(currentWarning);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public WarningResponse Get(GetWarningRequest request)
        {
            try
            {
                var warning = _warningRepository.FindBy(request.Id);
                warning.ThrowExceptionIfRecordIsNull();
                return _warningFactory.Execute(warning);
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
                warning.ThrowExceptionIfRecordIsNull();
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
                warning.ThrowExceptionIfRecordIsNull();
                warning.Status.ThrowExceptionIfIsSameStatus(request.Status);
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