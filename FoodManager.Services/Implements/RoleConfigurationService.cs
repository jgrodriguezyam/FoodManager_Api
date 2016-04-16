using System.Collections.Generic;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.RoleConfigurations;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.RoleConfigurations;
using FoodManager.Services.Factories.Interfaces;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class RoleConfigurationService : IRoleConfigurationService
    {
        private readonly IRoleConfigurationQuery _roleConfigurationQuery;
        private readonly IRoleConfigurationRepository _roleConfigurationRepository;
        private readonly IRoleConfigurationValidator _roleConfigurationValidator;
        private readonly IRoleConfigurationFactory _roleConfigurationFactory;

        public RoleConfigurationService(IRoleConfigurationQuery roleConfigurationQuery, IRoleConfigurationRepository roleConfigurationRepository, IRoleConfigurationValidator roleConfigurationValidator, IRoleConfigurationFactory roleConfigurationFactory)
        {
            _roleConfigurationQuery = roleConfigurationQuery;
            _roleConfigurationRepository = roleConfigurationRepository;
            _roleConfigurationValidator = roleConfigurationValidator;
            _roleConfigurationFactory = roleConfigurationFactory;
        }

        public FindRoleConfigurationsResponse Find(FindRoleConfigurationsRequest request)
        {
            try
            {
                _roleConfigurationQuery.WithRole(request.RoleId);
                _roleConfigurationQuery.WithPermission(request.PermissionId);
                _roleConfigurationQuery.WithAccessLevel(request.AccessLevelId);
                _roleConfigurationQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _roleConfigurationQuery.TotalRecords();
                _roleConfigurationQuery.Paginate(request.StartPage, request.EndPage);
                var roleConfigurations = _roleConfigurationQuery.Execute();

                return new FindRoleConfigurationsResponse
                {
                    RoleConfigurations = TypeAdapter.Adapt<List<RoleConfigurationResponse>>(roleConfigurations),
                    TotalRecords = totalRecords
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public CreateResponse Create(RoleConfigurationRequest request)
        {
            try
            {
                var roleConfiguration = TypeAdapter.Adapt<RoleConfiguration>(request);
                _roleConfigurationValidator.ValidateAndThrowException(roleConfiguration, "Base,Create");
                _roleConfigurationRepository.Add(roleConfiguration);
                return new CreateResponse(roleConfiguration.Id);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Update(RoleConfigurationRequest request)
        {
            try
            {
                var currentRoleConfiguration = _roleConfigurationRepository.FindBy(request.Id);
                currentRoleConfiguration.ThrowExceptionIfRecordIsNull();
                var RoleConfigurstionToCopy = TypeAdapter.Adapt<RoleConfiguration>(request);
                TypeAdapter.Adapt(RoleConfigurstionToCopy, currentRoleConfiguration);
                _roleConfigurationValidator.ValidateAndThrowException(currentRoleConfiguration, "Base,Update");
                _roleConfigurationRepository.Update(currentRoleConfiguration);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public DTO.RoleConfiguration Get(GetRoleConfigurationRequest request)
        {
            try
            {
                var roleConfiguration = _roleConfigurationRepository.FindBy(request.Id);
                roleConfiguration.ThrowExceptionIfRecordIsNull();
                return _roleConfigurationFactory.Execute(roleConfiguration);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Delete(DeleteRoleConfigurationRequest request)
        {
            try
            {
                var roleConfiguration = _roleConfigurationRepository.FindBy(request.Id);
                roleConfiguration.ThrowExceptionIfRecordIsNull();
                _roleConfigurationRepository.Remove(roleConfiguration);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}