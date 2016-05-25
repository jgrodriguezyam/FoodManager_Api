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
    public class DealerSaucerValidator : BaseValidator<DealerSaucer>, IDealerSaucerValidator
    {
        private readonly IDealerRepository _dealerRepository;
        private readonly ISaucerRepository _saucerRepository;
        private readonly IDealerSaucerRepository _dealerSaucerRepository;

        public DealerSaucerValidator(IDealerRepository dealerRepository, ISaucerRepository saucerRepository, IDealerSaucerRepository dealerSaucerRepository)
        {
            _dealerRepository = dealerRepository;
            _saucerRepository = saucerRepository;
            _dealerSaucerRepository = dealerSaucerRepository;

            RuleSet("Base", () =>
            {
                RuleFor(dealerSaucer => dealerSaucer.DealerId).Must(dealerId => dealerId.IsNotZero()).WithMessage("Tienes que elegir un distribuidor");
                RuleFor(dealerSaucer => dealerSaucer.SaucerId).Must(saucerId => saucerId.IsNotZero()).WithMessage("Tienes que elegir un platillo");
                Custom(ReferencesValidate);
            });
        }
        
        public ValidationFailure ReferencesValidate(DealerSaucer dealerSaucer, ValidationContext<DealerSaucer> context)
        {
            var dealer = _dealerRepository.FindBy(dealerSaucer.DealerId);
            if (dealer.IsNull() || dealer.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("DealerSaucer", "El distribuidor esta desactivado o no existe");

            var saucer = _saucerRepository.FindBy(dealerSaucer.SaucerId);
            if (saucer.IsNull() || saucer.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("DealerSaucer", "El platillo esta desactivado o no existe");

            var dealerSaucerRetrieved = _dealerSaucerRepository.FindBy(deaSau => deaSau.DealerId == dealerSaucer.DealerId && deaSau.SaucerId == dealerSaucer.SaucerId);
            if (dealerSaucerRetrieved.IsNotEmpty())
                return new ValidationFailure("DealerSaucer", "Ya existe la relacion");

            return null;
        }
    }
}