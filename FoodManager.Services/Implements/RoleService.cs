using System.Collections.Generic;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Roles;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.Roles;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class RoleService : IRoleService
    {
        private readonly IRoleQuery _roleQuery;
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleValidator _roleValidator;

        public RoleService(IRoleQuery roleQuery, IRoleRepository roleRepository, IRoleValidator roleValidator)
        {
            _roleQuery = roleQuery;
            _roleRepository = roleRepository;
            _roleValidator = roleValidator;
        }

        public FindRolesResponse Find(FindRolesRequest request)
        {
            try
            {
                _roleQuery.WithOnlyActivated(true);
                _roleQuery.WithOnlyStatusActivated(request.OnlyStatusActivated);
                _roleQuery.WithOnlyStatusDeactivated(request.OnlyStatusDeactivated);
                _roleQuery.WithName(request.Name);
                _roleQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _roleQuery.TotalRecords();
                _roleQuery.Paginate(request.StartPage, request.EndPage);
                var roles = _roleQuery.Execute();

                return new FindRolesResponse
                {
                    Roles = TypeAdapter.Adapt<List<RoleResponse>>(roles),
                    TotalRecords = totalRecords
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public CreateResponse Create(RoleRequest request)
        {
            try
            {
                var role = TypeAdapter.Adapt<Role>(request);
                _roleValidator.ValidateAndThrowException(role, "Base");
                _roleRepository.Add(role);
                return new CreateResponse(role.Id);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Update(RoleRequest request)
        {
            try
            {
                var currentRole = _roleRepository.FindBy(request.Id);
                currentRole.ThrowExceptionIfRecordIsNull();
                var roleToCopy = TypeAdapter.Adapt<Role>(request);
                TypeAdapter.Adapt(roleToCopy, currentRole);
                _roleValidator.ValidateAndThrowException(currentRole, "Base");
                _roleRepository.Update(currentRole);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public RoleResponse Get(GetRoleRequest request)
        {
            try
            {
                var role = _roleRepository.FindBy(request.Id);
                role.ThrowExceptionIfRecordIsNull();
                return TypeAdapter.Adapt<RoleResponse>(role);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Delete(DeleteRoleRequest request)
        {
            try
            {
                var role = _roleRepository.FindBy(request.Id);
                role.ThrowExceptionIfRecordIsNull();
                _roleValidator.ValidateAndThrowException(role, "Base");
                var isReference = _roleRepository.IsReference(request.Id);
                isReference.ThrowExceptionIfIsReference();
                _roleRepository.Remove(role);
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
                var role = _roleRepository.FindBy(request.Id);
                role.ThrowExceptionIfRecordIsNull();
                role.Status.ThrowExceptionIfIsSameStatus(request.Status);
                _roleValidator.ValidateAndThrowException(role, "Base");
                role.Status = request.Status;
                _roleRepository.Update(role);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse IsReference(IsReferenceRequest request)
        {
            try
            {
                var isReference = _roleRepository.IsReference(request.Id);
                return new SuccessResponse { IsSuccess = isReference };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}