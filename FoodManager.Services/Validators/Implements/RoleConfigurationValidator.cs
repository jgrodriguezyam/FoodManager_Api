using FluentValidation;
using FluentValidation.Results;
using FoodManager.Infrastructure.Collections;
using FoodManager.Infrastructure.Objects;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Validators.Implements
{
    public class RoleConfigurationValidator : BaseValidator<RoleConfiguration>, IRoleConfigurationValidator
    {
        private readonly IRoleConfigurationRepository _roleConfigurationRepository;
        private readonly IPermissionAccessLevelRepository _permissionAccessLevelRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IAccessLevelRepository _accessLevelRepository;

        public RoleConfigurationValidator(IRoleConfigurationRepository roleConfigurationRepository, IPermissionAccessLevelRepository permissionAccessLevelRepository, IRoleRepository roleRepository, IPermissionRepository permissionRepository, IAccessLevelRepository accessLevelRepository)
        {
            _roleConfigurationRepository = roleConfigurationRepository;
            _permissionAccessLevelRepository = permissionAccessLevelRepository;
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _accessLevelRepository = accessLevelRepository;

            RuleSet("Base", () =>
                            {
                                RuleFor(roleConfiguration => roleConfiguration.RoleId).NotNull().NotEmpty();
                                RuleFor(roleConfiguration => roleConfiguration.PermissionId).NotNull().NotEmpty();
                                RuleFor(roleConfiguration => roleConfiguration.AccessLevelId).NotNull().NotEmpty();
                                Custom(CheckIsAccessLevelBelongToPermission);
                                Custom(ReferencesValidate);
                            });

            RuleSet("Create", () =>
            {
                Custom(CreateRoleConfigurationValidate);
            });

            RuleSet("Update", () =>
            {
                Custom(UpdateRoleConfigurationValidate);
            });
        }

        public ValidationFailure ReferencesValidate(RoleConfiguration roleConfiguration, ValidationContext<RoleConfiguration> context)
        {
            var role = _roleRepository.FindBy(roleConfiguration.RoleId);
            if (role.IsNull())
                return new ValidationFailure("RoleConfiguration", "El rol no existe");

            var permission = _permissionRepository.FindBy(roleConfiguration.PermissionId);
            if (permission.IsNull())
                return new ValidationFailure("RoleConfiguration", "El permiso no existe");

            var accessLevel = _accessLevelRepository.FindBy(roleConfiguration.AccessLevelId);
            if (accessLevel.IsNull())
                return new ValidationFailure("RoleConfiguration", "El nivel de acceso no existe");

            return null;
        }

        public ValidationFailure CheckIsAccessLevelBelongToPermission(RoleConfiguration roleConfiguration, ValidationContext<RoleConfiguration> context)
        {
            var permissionAccessLevels = _permissionAccessLevelRepository.FindBy(permissionAccessLevel => permissionAccessLevel.PermissionId == roleConfiguration.PermissionId && permissionAccessLevel.AccessLevelId == roleConfiguration.AccessLevelId);
            if (permissionAccessLevels.IsEmpty())
                return new ValidationFailure("RoleConfiguration", string.Format("El permiso {0} no tiene el nivel de acceso {1} asignado", roleConfiguration.PermissionId, roleConfiguration.AccessLevelId));
            return null;
        }

        public ValidationFailure CreateRoleConfigurationValidate(RoleConfiguration roleConfiguration, ValidationContext<RoleConfiguration> context)
        {
            var roleConfigurationRetrieve = _roleConfigurationRepository.FindBy(roleConfig => roleConfig.RoleId == roleConfiguration.RoleId && roleConfig.PermissionId == roleConfiguration.PermissionId && roleConfig.AccessLevelId == roleConfiguration.AccessLevelId);
            if (roleConfigurationRetrieve.IsNotEmpty())
                return new ValidationFailure("RoleConfiguration", string.Format("Ya existe la configuración {0}, {1}, {2}", roleConfiguration.RoleId, roleConfiguration.PermissionId, roleConfiguration.AccessLevelId));
            return null;
        }

        public ValidationFailure UpdateRoleConfigurationValidate(RoleConfiguration roleConfiguration, ValidationContext<RoleConfiguration> context)
        {
            var roleConfigurationRetrieve = _roleConfigurationRepository.FindBy(roleConfig => roleConfig.RoleId == roleConfiguration.RoleId && roleConfig.PermissionId == roleConfiguration.PermissionId && roleConfig.AccessLevelId == roleConfiguration.AccessLevelId && roleConfig.Id != roleConfiguration.Id);
            if (roleConfigurationRetrieve.IsNotEmpty())
                return new ValidationFailure("RoleConfiguration", string.Format("Ya existe la configuración {0}, {1}, {2}", roleConfiguration.RoleId, roleConfiguration.PermissionId, roleConfiguration.AccessLevelId));
            return null;
        }


    }
}