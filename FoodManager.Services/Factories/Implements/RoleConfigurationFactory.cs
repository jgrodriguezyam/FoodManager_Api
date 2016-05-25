using FastMapper;
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

        public DTO.RoleConfiguration Execute(RoleConfiguration roleConfiguration)
        {
            var roleConfigurationDto = TypeAdapter.Adapt<DTO.RoleConfiguration>(roleConfiguration);
            var role = _roleRepository.FindBy(roleConfiguration.RoleId);
            roleConfigurationDto.Role = TypeAdapter.Adapt<DTO.Role>(role);
            var permission = _permissionRepository.FindBy(roleConfiguration.PermissionId);
            roleConfigurationDto.Permission = TypeAdapter.Adapt<DTO.Permission>(permission);
            var accessLevel = _accessLevelRepository.FindBy(roleConfiguration.AccessLevelId);
            roleConfigurationDto.AccessLevel = TypeAdapter.Adapt<DTO.AccessLevel>(accessLevel);
            return roleConfigurationDto;
        }
    }
}