using FluentValidation;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Validators.Implements
{
    public class JobValidator : BaseValidator<Job>, IJobValidator
    {
        public JobValidator()
        {
            RuleSet("Base", () =>
            {
                RuleFor(job => job.Name).NotNull().NotEmpty();
            });
        }
    }
}