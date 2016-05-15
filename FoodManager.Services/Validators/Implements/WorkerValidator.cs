using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Enums;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Objects;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Model.Enums;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Validators.Implements
{
    public class WorkerValidator : BaseValidator<Worker>, IWorkerValidator
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IDealerRepository _dealerRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IBranchRepository _branchRepository;

        public WorkerValidator(IDepartmentRepository departmentRepository, IJobRepository jobRepository, IDealerRepository dealerRepository, IRoleRepository roleRepository, IBranchRepository branchRepository)
        {
            _departmentRepository = departmentRepository;
            _jobRepository = jobRepository;
            _dealerRepository = dealerRepository;
            _roleRepository = roleRepository;
            _branchRepository = branchRepository;

            RuleSet("Base", () =>
            {
                RuleFor(worker => worker.Code).NotNull().NotEmpty();
                RuleFor(worker => worker.FirstName).NotNull().NotEmpty();
                RuleFor(worker => worker.LastName).NotNull().NotEmpty();
                RuleFor(worker => worker.Email).NotNull().NotEmpty();
                RuleFor(worker => worker.Imss).NotNull().NotEmpty();
                RuleFor(worker => worker.Gender).Must(gender => gender.IsNotZero()).WithMessage("Tienes que elegir un genero");
                RuleFor(worker => worker.Badge).NotNull().NotEmpty();
                RuleFor(worker => worker.DepartmentId).Must(departmentId => departmentId.IsNotZero()).WithMessage("Tienes que elegir un departamento");
                RuleFor(worker => worker.JobId).Must(jobId => jobId.IsNotZero()).WithMessage("Tienes que elegir un puesto");
                RuleFor(worker => worker.DealerId).Must(dealerId => dealerId.IsNotZero()).WithMessage("Tienes que elegir un distribuidor");
                RuleFor(worker => worker.RoleId).Must(roleId => roleId.IsNotZero()).WithMessage("Tienes que elegir un rol");
                RuleFor(worker => worker.BranchId).Must(branchId => branchId.IsNotZero()).WithMessage("Tienes que elegir una sucursal");
                Custom(ReferencesValidate);
            });
        }

        public ValidationFailure ReferencesValidate(Worker worker, ValidationContext<Worker> context)
        {
            var genderType = new GenderType().ConvertToCollection().FirstOrDefault(genderTp => genderTp.Value == worker.Gender);
            if (genderType.IsNull())
                return new ValidationFailure("Worker", "El tipo de genero no existe");

            var department = _departmentRepository.FindBy(worker.DepartmentId);
            if (department.IsNull() || department.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("Worker", "El departamento esta desactivado o no existe");

            var job = _jobRepository.FindBy(worker.JobId);
            if (job.IsNull() || job.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("Worker", "El puesto esta desactivado o no existe");

            var dealer = _dealerRepository.FindBy(worker.DealerId);
            if (dealer.IsNull() || dealer.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("Worker", "El distribuidor esta desactivado o no existe");

            var role = _roleRepository.FindBy(worker.RoleId);
            if (role.IsNull() || role.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("Worker", "El rol esta desactivado o no existe");

            var branch = _branchRepository.FindBy(worker.BranchId);
            if (branch.IsNull() || branch.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("Worker", "La sucursal esta desactivada o no existe");

            return null;
        }
    }
}