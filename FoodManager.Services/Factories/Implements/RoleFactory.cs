using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Roles;
using FoodManager.Infrastructure.Integers;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class RoleFactory : IRoleFactory
    {
        private readonly IUserRepository _userRepository;
        private readonly IWorkerRepository _workerRepository;
        private readonly IRoleConfigurationRepository _roleConfigurationRepository;

        public RoleFactory(IUserRepository userRepository, IWorkerRepository workerRepository, IRoleConfigurationRepository roleConfigurationRepository)
        {
            _userRepository = userRepository;
            _workerRepository = workerRepository;
            _roleConfigurationRepository = roleConfigurationRepository;
        }

        public RoleResponse Execute(Role role)
        {
            return AppendProperties(new[] { role }).FirstOrDefault();
        }

        public IEnumerable<RoleResponse> Execute(IEnumerable<Role> roles)
        {
            return AppendProperties(roles);
        }

        private IEnumerable<RoleResponse> AppendProperties(IEnumerable<Role> roles)
        {
            var rolesResponse = TypeAdapter.Adapt<List<RoleResponse>>(roles);
            var users = _userRepository.FindBy(user => user.IsActive);
            var workers = _workerRepository.FindBy(worker => worker.IsActive);
            var roleConfigurations = _roleConfigurationRepository.FindBy(roleConfiguraion => roleConfiguraion.IsActive);

            rolesResponse.ForEach(roleResponse =>
            {
                var role = roles.First(roleModel => roleModel.Id == roleResponse.Id);
                var amountOfReferences = users.Count(user => user.RoleId == role.Id);
                amountOfReferences += workers.Count(worker => worker.RoleId == role.Id);
                amountOfReferences += roleConfigurations.Count(roleConfiguration => roleConfiguration.RoleId == role.Id);
                roleResponse.IsReference = amountOfReferences.IsNotZero();
            });

            return rolesResponse;
        }
    }
}