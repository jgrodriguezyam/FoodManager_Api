using System.Linq;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.IngredientGroups;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Infrastructure.Files;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.IngredientGroups;
using FoodManager.Services.Factories.Interfaces;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class IngredientGroupService : IIngredientGroupService
    {
        private readonly IIngredientGroupQuery _ingredientGroupQuery;
        private readonly IIngredientGroupRepository _ingredientGroupRepository;
        private readonly IIngredientGroupValidator _ingredientGroupValidator;
        private readonly IStorageProvider _storageProvider;
        private readonly IIngredientGroupFactory _ingredientGroupFactory;

        public IngredientGroupService(IIngredientGroupQuery ingredientGroupQuery, IIngredientGroupRepository ingredientGroupRepository, IIngredientGroupValidator ingredientGroupValidator, IStorageProvider storageProvider, IIngredientGroupFactory ingredientGroupFactory)
        {
            _ingredientGroupQuery = ingredientGroupQuery;
            _ingredientGroupRepository = ingredientGroupRepository;
            _ingredientGroupValidator = ingredientGroupValidator;
            _storageProvider = storageProvider;
            _ingredientGroupFactory = ingredientGroupFactory;
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
                    IngredientGroups = _ingredientGroupFactory.Execute(ingredientGroups).ToList(),
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

        public IngredientGroupResponse Get(GetIngredientGroupRequest request)
        {
            try
            {
                var ingredientGroup = _ingredientGroupRepository.FindBy(request.Id);
                ingredientGroup.ThrowExceptionIfRecordIsNull();
                return _ingredientGroupFactory.Execute(ingredientGroup);
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
                isReference.ThrowExceptionIfIsReference();
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
                ingredientGroup.Status.ThrowExceptionIfIsSameStatus(request.Status);
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

        public SuccessResponse Csv(CsvRequest request, File file)
        {
            try
            {
                var fileName = _storageProvider.Save(file);
                var ingredientGroups = _ingredientGroupFactory.FromCsv(fileName);
                ingredientGroups.ForEach(ingredientGroup => { _ingredientGroupValidator.ValidateAndThrowException(ingredientGroup, "Base"); });
                _ingredientGroupRepository.AddAll(ingredientGroups);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}