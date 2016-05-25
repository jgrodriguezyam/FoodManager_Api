using FluentValidation;
using FluentValidation.Results;
using FoodManager.Infrastructure.Collections;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Objects;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Validators.Implements
{
    public class BranchDealerValidator : BaseValidator<BranchDealer>, IBranchDealerValidator
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IDealerRepository _dealerRepository;
        private readonly IBranchDealerRepository _branchDealerRepository;

        public BranchDealerValidator(IBranchRepository branchRepository, IDealerRepository dealerRepository, IBranchDealerRepository branchDealerRepository)
        {
            _branchRepository = branchRepository;
            _dealerRepository = dealerRepository;
            _branchDealerRepository = branchDealerRepository;

            RuleSet("Base", () =>
            {
                RuleFor(branchDealer => branchDealer.BranchId).Must(branchId => branchId.IsNotZero()).WithMessage("Tienes que elegir una sucursal");
                RuleFor(branchDealer => branchDealer.DealerId).Must(dealerId => dealerId.IsNotZero()).WithMessage("Tienes que elegir un distribuidor");
                Custom(ReferencesValidate);
            });
        }

        public ValidationFailure ReferencesValidate(BranchDealer branchDealer, ValidationContext<BranchDealer> context)
        {
            var branch = _branchRepository.FindBy(branchDealer.BranchId);
            if (branch.IsNull() || branch.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("BranchDealer", "La sucursal esta desactivada o no existe");

            var dealer = _dealerRepository.FindBy(branchDealer.DealerId);
            if (dealer.IsNull() || dealer.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("BranchDealer", "El distribuidor esta desactivado o no existe");

            var branchDealerRetrieved = _branchDealerRepository.FindBy(braDea => braDea.BranchId == branchDealer.BranchId && braDea.DealerId == branchDealer.DealerId);
            if (branchDealerRetrieved.IsNotEmpty())
                return new ValidationFailure("BranchDealer", "Ya existe la relacion");

            return null;
        }
    }
}