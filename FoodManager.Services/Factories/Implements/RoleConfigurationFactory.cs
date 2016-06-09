using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.AccessLevels;
using FoodManager.DTO.Message.Permissions;
using FoodManager.DTO.Message.RoleConfigurations;
using FoodManager.DTO.Message.Roles;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class RoleConfigurationFactory : IRoleConfigurationFactory
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IAccessLevelRepository _accessLevelRepository;

        public RoleConfigurationFactory(IRoleRepository roleRepository, IPermissionRepository permissionRepository, IAccessLevelRepository accessLevelRepository)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _accessLevelRepository = accessLevelRepository;
        }

        public RoleConfigurationResponse Execute(RoleConfiguration roleConfiguration)
        {
            var roleConfigurationResponse = TypeAdapter.Adapt<RoleConfigurationResponse>(roleConfiguration);
            var role = _roleRepository.FindBy(roleConfiguration.RoleId);
            roleConfigurationResponse.Role = TypeAdapter.Adapt<RoleResponse>(role);
            var permission = _permissionRepository.FindBy(roleConfiguration.PermissionId);
            roleConfigurationResponse.Permission = TypeAdapter.Adapt<PermissionResponse>(permission);
            var accessLevel = _accessLevelRepository.FindBy(roleConfiguration.AccessLevelId);
            roleConfigurationResponse.AccessLevel = TypeAdapter.Adapt<AccessLevelResponse>(accessLevel);
            return roleConfigurationResponse;
        }

        public IEnumerable<RoleConfigurationResponse> Execute(IEnumerable<RoleConfiguration> roleConfigurations)
        {
            var roles = _roleRepository.FindAll();
            var permissions = _permissionRepository.FindAll();
            var accessLevels = _accessLevelRepository.FindAll();
            return roleConfigurations.Select(roleConfiguration => new RoleConfigurationResponse
                                                                  {
                                                                      Id = roleConfiguration.Id,
                                                                      Role = TypeAdapter.Adapt<RoleResponse>(roles.FirstOrDefault(role => role.Id == roleConfiguration.RoleId)),
                                                                      Permission = TypeAdapter.Adapt<PermissionResponse>(permissions.FirstOrDefault(permission => permission.Id == roleConfiguration.PermissionId)),
                                                                      AccessLevel = TypeAdapter.Adapt<AccessLevelResponse>(accessLevels.FirstOrDefault(accessLevel => accessLevel.Id == roleConfiguration.AccessLevelId))
                                                                  });
        }
    }
}