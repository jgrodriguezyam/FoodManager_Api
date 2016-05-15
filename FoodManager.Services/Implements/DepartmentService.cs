using System.Collections.Generic;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Departments;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.Departments;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentQuery _departmentQuery;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDepartmentValidator _departmentValidator;
        
        public DepartmentService(IDepartmentQuery departmentQuery, IDepartmentRepository departmentRepository, IDepartmentValidator departmentValidator)
        {
            _departmentQuery = departmentQuery;
            _departmentRepository = departmentRepository;
            _departmentValidator = departmentValidator;
        }

        public FindDepartmentsResponse Find(FindDepartmentsRequest request)
        {
            try
            {
                _departmentQuery.WithOnlyActivated(true);
                _departmentQuery.WithOnlyStatusActivated(request.OnlyStatusActivated);
                _departmentQuery.WithOnlyStatusDeactivated(request.OnlyStatusDeactivated);
                _departmentQuery.WithName(request.Name);
                _departmentQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _departmentQuery.TotalRecords();
                _departmentQuery.Paginate(request.StartPage, request.EndPage);
                var departments = _departmentQuery.Execute();

                return new FindDepartmentsResponse
                {
                    Departments = TypeAdapter.Adapt<List<DepartmentResponse>>(departments),
                    TotalRecords = totalRecords
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public CreateResponse Create(DepartmentRequest request)
        {
            try
            {
                var department = TypeAdapter.Adapt<Department>(request);
                _departmentValidator.ValidateAndThrowException(department, "Base");
                _departmentRepository.Add(department);
                return new CreateResponse(department.Id);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Update(DepartmentRequest request)
        {
            try
            {
                var currentDepartment = _departmentRepository.FindBy(request.Id);
                currentDepartment.ThrowExceptionIfRecordIsNull();
                var departmentToCopy = TypeAdapter.Adapt<Department>(request);
                TypeAdapter.Adapt(departmentToCopy, currentDepartment);
                _departmentValidator.ValidateAndThrowException(currentDepartment, "Base");
                _departmentRepository.Update(currentDepartment);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public DTO.Department Get(GetDepartmentRequest request)
        {
            try
            {
                var department = _departmentRepository.FindBy(request.Id);
                department.ThrowExceptionIfRecordIsNull();
                return TypeAdapter.Adapt<DTO.Department>(department);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Delete(DeleteDepartmentRequest request)
        {
            try
            {
                var department = _departmentRepository.FindBy(request.Id);
                department.ThrowExceptionIfRecordIsNull();
                var isReference = _departmentRepository.IsReference(request.Id);
                isReference.ThrowExceptionIfIsReference();
                _departmentRepository.Remove(department);
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
                var department = _departmentRepository.FindBy(request.Id);
                department.ThrowExceptionIfRecordIsNull();
                department.Status.ThrowExceptionIfIsSameStatus(request.Status);
                department.Status = request.Status;
                _departmentRepository.Update(department);
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
                var isReference = _departmentRepository.IsReference(request.Id);
                return new SuccessResponse { IsSuccess = isReference };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}