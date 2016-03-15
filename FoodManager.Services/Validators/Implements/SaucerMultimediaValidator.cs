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
    public class SaucerMultimediaValidator : BaseValidator<SaucerMultimedia>, ISaucerMultimediaValidator
    {
        private readonly ISaucerRepository _saucerRepository;

        public SaucerMultimediaValidator(ISaucerRepository saucerRepository)
        {
            _saucerRepository = saucerRepository;

            RuleSet("Base", () =>
            {
                RuleFor(saucerMultimedia => saucerMultimedia.SaucerId).Must(saucerId => saucerId.IsNotZero()).WithMessage("Tienes que elegir un platillo");
                Custom(ReferencesValidate);
            });
        }

        public ValidationFailure ReferencesValidate(SaucerMultimedia saucerMultimedia, ValidationContext<SaucerMultimedia> context)
        {
            var saucer = _saucerRepository.FindBy(saucerMultimedia.SaucerId);
            if (saucer.IsNull() || saucer.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("SaucerMultimedia", "El platillo esta desactivado o no existe");

            return null;
        }
    }
}