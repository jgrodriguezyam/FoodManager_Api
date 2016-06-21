using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Roles;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class RoleFactory : IRoleFactory
    {
        private readonly IRoleRepository _roleRepository;

        public RoleFactory(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public RoleResponse Execute(Role role)
        {
            var roleResponse = TypeAdapter.Adapt<RoleResponse>(role);
            roleResponse.IsReference = _roleRepository.IsReference(role.Id);
            return roleResponse;
        }

        public IEnumerable<RoleResponse> Execute(IEnumerable<Role> roles)
        {
            return roles.Select(Execute);
        }
    }
}