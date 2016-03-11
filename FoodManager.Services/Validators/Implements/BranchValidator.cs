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
    public class BranchValidator : BaseValidator<Branch>, IBranchValidator
    {
        private readonly ICompanyRepository _companyRepository;
        
        public BranchValidator(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;

            RuleSet("Base", () =>
            {
                RuleFor(branch => branch.Name).NotNull().NotEmpty();
                RuleFor(branch => branch.Code).NotNull().NotEmpty();
                RuleFor(branch => branch.CompanyId).Must(companyId => companyId.IsNotZero()).WithMessage("Tienes que elegir una compania");
                Custom(ReferencesValidate);
            });
        }

        public ValidationFailure ReferencesValidate(Branch branch, ValidationContext<Branch> context)
        {
            var company = _companyRepository.FindBy(branch.CompanyId);
            if (company.IsNull() || company.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("Branch", "La compania esta desactivada o no existe");

            return null;
        }
    }
}