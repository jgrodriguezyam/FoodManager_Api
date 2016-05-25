using System.Collections.Generic;
using FastMapper;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.AccessLevels;
using FoodManager.DTO.Message.RoleConfigurations;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.AccessLevels;
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
        private readonly IAccessLevelQuery _accessLevelQuery;

        public RoleConfigurationService(IRoleConfigurationQuery roleConfigurationQuery, IRoleConfigurationRepository roleConfigurationRepository, IRoleConfigurationValidator roleConfigurationValidator, IRoleConfigurationFactory roleConfigurationFactory, IAccessLevelQuery accessLevelQuery)
        {
            _roleConfigurationQuery = roleConfigurationQuery;
            _roleConfigurationRepository = roleConfigurationRepository;
            _roleConfigurationValidator = roleConfigurationValidator;
            _roleConfigurationFactory = roleConfigurationFactory;
            _accessLevelQuery = accessLevelQuery;
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
                var roleConfigurstionToCopy = TypeAdapter.Adapt<RoleConfiguration>(request);
                TypeAdapter.Adapt(roleConfigurstionToCopy, currentRoleConfiguration);
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

        public FindAccessLevelsResponse Find(FindAccessLevelsRequest request)
        {
            try
            {
                _accessLevelQuery.WithPermission(request.PermissionId);
                var accessLevels = _accessLevelQuery.Execute();

                return new FindAccessLevelsResponse
                {
                    AccessLevels = TypeAdapter.Adapt<List<AccessLevelResponse>>(accessLevels)
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}