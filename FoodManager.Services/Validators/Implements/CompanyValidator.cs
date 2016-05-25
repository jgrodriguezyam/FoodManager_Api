using FluentValidation;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Services.Validators.Interfaces;
using FluentValidation.Results;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Objects;
using FoodManager.Model.IRepositories;

namespace FoodManager.Services.Validators.Implements
{
    public class CompanyValidator : BaseValidator<Company>, ICompanyValidator
    {
        private readonly IRegionRepository _regionRepository;

        public CompanyValidator(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;

            RuleSet("Base", () =>
            {
                RuleFor(company => company.Name).NotNull().NotEmpty();
                RuleFor(company => company.RegionId).Must(regionId => regionId.IsNotZero()).WithMessage("Tienes que elegir una region");
                Custom(ReferencesValidate);
            });
        }

        public ValidationFailure ReferencesValidate(Company company, ValidationContext<Company> context)
        {
            var region = _regionRepository.FindBy(company.RegionId);
            if (region.IsNull() || region.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("Company", "La region esta desactivada o no existe");

            return null;
        }
    }
}