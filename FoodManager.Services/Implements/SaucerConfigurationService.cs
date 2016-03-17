using System.Collections.Generic;
using System.Net;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.SaucerConfigurations;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.SaucerConfigurations;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class SaucerConfigurationService : ISaucerConfigurationService
    {
        private readonly ISaucerConfigurationQuery _saucerConfigurationQuery;
        private readonly ISaucerConfigurationRepository _saucerConfigurationRepository;
        private readonly ISaucerConfigurationValidator _saucerConfigurationValidator;

        public SaucerConfigurationService(ISaucerConfigurationQuery saucerConfigurationQuery, ISaucerConfigurationRepository saucerConfigurationRepository, ISaucerConfigurationValidator saucerConfigurationValidator)
        {
            _saucerConfigurationQuery = saucerConfigurationQuery;
            _saucerConfigurationRepository = saucerConfigurationRepository;
            _saucerConfigurationValidator = saucerConfigurationValidator;
        }

        public FindSaucerConfigurationsResponse Find(FindSaucerConfigurationsRequest request)
        {
            try
            {
                _saucerConfigurationQuery.WithOnlyActivated(true);
                _saucerConfigurationQuery.WithOnlyStatusActivated(request.OnlyStatusActivated);
                _saucerConfigurationQuery.WithOnlyStatusDeactivated(request.OnlyStatusDeactivated);
                _saucerConfigurationQuery.WithSaucer(request.SaucerId);
                _saucerConfigurationQuery.WithIngredient(request.IngredientId);
                _saucerConfigurationQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _saucerConfigurationQuery.TotalRecords();
                _saucerConfigurationQuery.Paginate(request.StartPage, request.EndPage);
                var saucers = _saucerConfigurationQuery.Execute();

                return new FindSaucerConfigurationsResponse
                {
                    SaucerConfigurations = TypeAdapter.Adapt<List<SaucerConfigurationResponse>>(saucers),
                    TotalRecords = totalRecords
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public CreateResponse Create(SaucerConfigurationRequest request)
        {
            try
            {
                var saucerConfiguration = TypeAdapter.Adapt<SaucerConfiguration>(request);
                _saucerConfigurationValidator.ValidateAndThrowException(saucerConfiguration, "Base");
                _saucerConfigurationRepository.Add(saucerConfiguration);
                return new CreateResponse(saucerConfiguration.Id);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Update(SaucerConfigurationRequest request)
        {
            try
            {
                var currentSaucerConfiguration = _saucerConfigurationRepository.FindBy(request.Id);
                currentSaucerConfiguration.ThrowExceptionIfIsNull("Configuracion de platillo no encontrado");
                var saucerConfigurationToCopy = TypeAdapter.Adapt<SaucerConfiguration>(request);
                TypeAdapter.Adapt(saucerConfigurationToCopy, currentSaucerConfiguration);
                _saucerConfigurationValidator.ValidateAndThrowException(currentSaucerConfiguration, "Base");
                _saucerConfigurationRepository.Update(currentSaucerConfiguration);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public DTO.SaucerConfiguration Get(GetSaucerConfigurationRequest request)
        {
            try
            {
                var saucerConfiguration = _saucerConfigurationRepository.FindBy(request.Id);
                saucerConfiguration.ThrowExceptionIfIsNull("Configuracion de platillo no encontrado");
                return TypeAdapter.Adapt<DTO.SaucerConfiguration>(saucerConfiguration);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Delete(DeleteSaucerConfigurationRequest request)
        {
            try
            {
                var saucerConfiguration = _saucerConfigurationRepository.FindBy(request.Id);
                saucerConfiguration.ThrowExceptionIfIsNull("Configuracion de platillo no encontrado");
                _saucerConfigurationRepository.Remove(saucerConfiguration);
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
                var saucerConfiguration = _saucerConfigurationRepository.FindBy(request.Id);
                saucerConfiguration.ThrowExceptionIfIsNull("Configuracion de platillo no encontrado");
                if (saucerConfiguration.Status.Equals(request.Status))
                    ExceptionExtensions.ThrowStatusException(HttpStatusCode.Accepted, request.Status);
                saucerConfiguration.Status = request.Status;
                _saucerConfigurationRepository.Update(saucerConfiguration);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}