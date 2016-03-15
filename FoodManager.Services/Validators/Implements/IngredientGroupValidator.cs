using FluentValidation;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Validators.Implements
{
    public class IngredientGroupValidator : BaseValidator<IngredientGroup>, IIngredientGroupValidator
    {
        public IngredientGroupValidator()
        {
            RuleSet("Base", () =>
            {
                RuleFor(ingredientGroup => ingredientGroup.Name).NotNull().NotEmpty();
                RuleFor(ingredientGroup => ingredientGroup.Color).NotNull().NotEmpty();
            });
        }
    }
}