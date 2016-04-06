using FluentValidation;
using FluentValidation.Results;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Objects;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Validators.Implements
{
    public class IngredientValidator : BaseValidator<Ingredient>, IIngredientValidator
    {
        private readonly IIngredientGroupRepository _ingredientGroupRepository;

        public IngredientValidator(IIngredientGroupRepository ingredientGroupRepository)
        {
            _ingredientGroupRepository = ingredientGroupRepository;

            RuleSet("Base", () =>
            {
                RuleFor(ingredient => ingredient.Name).NotNull().NotEmpty();
                RuleFor(ingredient => ingredient.Amount).NotNull().NotEmpty();
                RuleFor(ingredient => ingredient.IngredientGroupId).Must(ingredientGroupId => ingredientGroupId.IsNotZero()).WithMessage("Tienes que elegir un grupo");
                Custom(ReferencesValidate);
            });
        }

        public ValidationFailure ReferencesValidate(Ingredient ingredient, ValidationContext<Ingredient> context)
        {
            var ingredientGroup = _ingredientGroupRepository.FindBy(ingredient.IngredientGroupId);
            if (ingredientGroup.IsNull() || ingredientGroup.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("Ingredient", "El grupo esta desactivado o no existe");

            return null;
        }
    }
}