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
    public class DepartmentValidator : BaseValidator<Department>, IDepartmentValidator
    {
        private readonly IBranchRepository _branchRepository;

        public DepartmentValidator(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;

            RuleSet("Base", () =>
            {
                RuleFor(department => department.Name).NotNull().NotEmpty();
                RuleFor(department => department.BranchId).Must(branchId => branchId.IsNotZero()).WithMessage("Tienes que elegir una sucursal");
                Custom(ReferencesValidate);
            });
        }

        public ValidationFailure ReferencesValidate(Department department, ValidationContext<Department> context)
        {
            var branch = _branchRepository.FindBy(department.BranchId);
            if (branch.IsNull() || branch.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("Department", "La sucursal esta desactivada o no existe");

            return null;
        }
    }
}