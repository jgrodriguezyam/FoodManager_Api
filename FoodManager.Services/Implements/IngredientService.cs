using System.Collections.Generic;
using System.Net;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Ingredients;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.Ingredients;
using FoodManager.Services.Factories.Interfaces;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientQuery _ingredientQuery;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IIngredientValidator _ingredientValidator;
        private readonly IIngredientFactory _ingredientFactory;

        public IngredientService(IIngredientQuery ingredientQuery, IIngredientRepository ingredientRepository, IIngredientValidator ingredientValidator, IIngredientFactory ingredientFactory)
        {
            _ingredientQuery = ingredientQuery;
            _ingredientRepository = ingredientRepository;
            _ingredientValidator = ingredientValidator;
            _ingredientFactory = ingredientFactory;
        }

        public FindIngredientsResponse Find(FindIngredientsRequest request)
        {
            try
            {
                _ingredientQuery.WithOnlyActivated(true);
                _ingredientQuery.WithOnlyStatusActivated(request.OnlyStatusActivated);
                _ingredientQuery.WithOnlyStatusDeactivated(request.OnlyStatusDeactivated);
                _ingredientQuery.WithIngredientGroup(request.IngredientGroupId);
                _ingredientQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _ingredientQuery.TotalRecords();
                _ingredientQuery.Paginate(request.StartPage, request.EndPage);
                var ingredients = _ingredientQuery.Execute();

                return new FindIngredientsResponse
                {
                    Ingredients = TypeAdapter.Adapt<List<IngredientResponse>>(ingredients),
                    TotalRecords = totalRecords
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public CreateResponse Create(IngredientRequest request)
        {
            try
            {
                var ingredient = TypeAdapter.Adapt<Ingredient>(request);
                _ingredientValidator.ValidateAndThrowException(ingredient, "Base");
                _ingredientRepository.Add(ingredient);
                return new CreateResponse(ingredient.Id);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Update(IngredientRequest request)
        {
            try
            {
                var currentIngredient = _ingredientRepository.FindBy(request.Id);
                currentIngredient.ThrowExceptionIfIsNull("Ingrediente no encontrado");
                var ingredientToCopy = TypeAdapter.Adapt<Ingredient>(request);
                TypeAdapter.Adapt(ingredientToCopy, currentIngredient);
                _ingredientValidator.ValidateAndThrowException(currentIngredient, "Base");
                _ingredientRepository.Update(currentIngredient);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public DTO.Ingredient Get(GetIngredientRequest request)
        {
            try
            {
                var ingredient = _ingredientRepository.FindBy(request.Id);
                ingredient.ThrowExceptionIfIsNull("Ingrediente no encontrado");
                return _ingredientFactory.Execute(ingredient);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Delete(DeleteIngredientRequest request)
        {
            try
            {
                var ingredient = _ingredientRepository.FindBy(request.Id);
                ingredient.ThrowExceptionIfIsNull("Ingrediente no encontrado");
                var isReference = _ingredientRepository.IsReference(request.Id);
                if (isReference)
                    ExceptionExtensions.ThrowIsReferenceException();
                _ingredientRepository.Remove(ingredient);
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
                var ingredient = _ingredientRepository.FindBy(request.Id);
                ingredient.ThrowExceptionIfIsNull("Ingrediente no encontrado");
                if (ingredient.Status.Equals(request.Status))
                    ExceptionExtensions.ThrowStatusException(HttpStatusCode.Accepted, request.Status);
                ingredient.Status = request.Status;
                _ingredientRepository.Update(ingredient);
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
                var isReference = _ingredientRepository.IsReference(request.Id);
                return new SuccessResponse { IsSuccess = isReference };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}