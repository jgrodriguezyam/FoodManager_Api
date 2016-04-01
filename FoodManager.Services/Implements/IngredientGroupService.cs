using System.Collections.Generic;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.IngredientGroups;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.IngredientGroups;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class IngredientGroupService : IIngredientGroupService
    {
        private readonly IIngredientGroupQuery _ingredientGroupQuery;
        private readonly IIngredientGroupRepository _ingredientGroupRepository;
        private readonly IIngredientGroupValidator _ingredientGroupValidator;

        public IngredientGroupService(IIngredientGroupQuery ingredientGroupQuery, IIngredientGroupRepository ingredientGroupRepository, IIngredientGroupValidator ingredientGroupValidator)
        {
            _ingredientGroupQuery = ingredientGroupQuery;
            _ingredientGroupRepository = ingredientGroupRepository;
            _ingredientGroupValidator = ingredientGroupValidator;
        }

        public FindIngredientGroupsResponse Find(FindIngredientGroupsRequest request)
        {
            try
            {
                _ingredientGroupQuery.WithOnlyActivated(true);
                _ingredientGroupQuery.WithOnlyStatusActivated(request.OnlyStatusActivated);
                _ingredientGroupQuery.WithOnlyStatusDeactivated(request.OnlyStatusDeactivated);
                _ingredientGroupQuery.WithName(request.Name);
                _ingredientGroupQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _ingredientGroupQuery.TotalRecords();
                _ingredientGroupQuery.Paginate(request.StartPage, request.EndPage);
                var ingredientGroups = _ingredientGroupQuery.Execute();

                return new FindIngredientGroupsResponse
                {
                    IngredientGroups = TypeAdapter.Adapt<List<IngredientGroupResponse>>(ingredientGroups),
                    TotalRecords = totalRecords
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public CreateResponse Create(IngredientGroupRequest request)
        {
            try
            {
                var ingredientGroup = TypeAdapter.Adapt<IngredientGroup>(request);
                _ingredientGroupValidator.ValidateAndThrowException(ingredientGroup, "Base");
                _ingredientGroupRepository.Add(ingredientGroup);
                return new CreateResponse(ingredientGroup.Id);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Update(IngredientGroupRequest request)
        {
            try
            {
                var currentIngredientGroup = _ingredientGroupRepository.FindBy(request.Id);
                currentIngredientGroup.ThrowExceptionIfRecordIsNull();
                var ingredientGroupToCopy = TypeAdapter.Adapt<IngredientGroup>(request);
                TypeAdapter.Adapt(ingredientGroupToCopy, currentIngredientGroup);
                _ingredientGroupValidator.ValidateAndThrowException(currentIngredientGroup, "Base");
                _ingredientGroupRepository.Update(currentIngredientGroup);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public DTO.IngredientGroup Get(GetIngredientGroupRequest request)
        {
            try
            {
                var ingredientGroup = _ingredientGroupRepository.FindBy(request.Id);
                ingredientGroup.ThrowExceptionIfRecordIsNull();
                return TypeAdapter.Adapt<DTO.IngredientGroup>(ingredientGroup);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Delete(DeleteIngredientGroupRequest request)
        {
            try
            {
                var ingredientGroup = _ingredientGroupRepository.FindBy(request.Id);
                ingredientGroup.ThrowExceptionIfRecordIsNull();
                var isReference = _ingredientGroupRepository.IsReference(request.Id);
                if (isReference)
                    ExceptionExtensions.ThrowIsReferenceException();
                _ingredientGroupRepository.Remove(ingredientGroup);
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
                var ingredientGroup = _ingredientGroupRepository.FindBy(request.Id);
                ingredientGroup.ThrowExceptionIfRecordIsNull();
                if (ingredientGroup.Status.Equals(request.Status))
                    ExceptionExtensions.ThrowStatusException(request.Status);
                ingredientGroup.Status = request.Status;
                _ingredientGroupRepository.Update(ingredientGroup);
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
                var isReference = _ingredientGroupRepository.IsReference(request.Id);
                return new SuccessResponse { IsSuccess = isReference };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}