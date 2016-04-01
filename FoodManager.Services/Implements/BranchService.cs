using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Branches;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.Branches;
using FoodManager.Services.Factories.Interfaces;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class BranchService : IBranchService
    {
        private readonly IBranchQuery _branchQuery;
        private readonly IBranchRepository _branchRepository;
        private readonly IBranchValidator _branchValidator;
        private readonly IBranchDealerRepository _branchDealerRepository;
        private readonly IBranchDealerValidator _branchDealerValidator;
        private readonly IBranchFactory _branchFactory;

        public BranchService(IBranchQuery branchQuery, IBranchRepository branchRepository, IBranchValidator branchValidator, IBranchDealerRepository branchDealerRepository, IBranchDealerValidator branchDealerValidator, IBranchFactory branchFactory)
        {
            _branchQuery = branchQuery;
            _branchRepository = branchRepository;
            _branchValidator = branchValidator;
            _branchDealerRepository = branchDealerRepository;
            _branchDealerValidator = branchDealerValidator;
            _branchFactory = branchFactory;
        }

        public FindBranchesResponse Find(FindBranchesRequest request)
        {
            try
            {
                _branchQuery.WithOnlyActivated(true);
                _branchQuery.WithOnlyStatusActivated(request.OnlyStatusActivated);
                _branchQuery.WithOnlyStatusDeactivated(request.OnlyStatusDeactivated);
                _branchQuery.WithCompany(request.CompanyId);
                _branchQuery.WithName(request.Name);
                _branchQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _branchQuery.TotalRecords();
                _branchQuery.Paginate(request.StartPage, request.EndPage);
                var branches = _branchQuery.Execute();

                return new FindBranchesResponse
                {
                    Branches = TypeAdapter.Adapt<List<BranchResponse>>(branches),
                    TotalRecords = totalRecords
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public CreateResponse Create(BranchRequest request)
        {
            try
            {
                var branch = TypeAdapter.Adapt<Branch>(request);
                _branchValidator.ValidateAndThrowException(branch, "Base");
                _branchRepository.Add(branch);
                return new CreateResponse(branch.Id);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Update(BranchRequest request)
        {
            try
            {
                var currentBranch = _branchRepository.FindBy(request.Id);
                currentBranch.ThrowExceptionIfRecordIsNull();
                var branchToCopy = TypeAdapter.Adapt<Branch>(request);
                TypeAdapter.Adapt(branchToCopy, currentBranch);
                _branchValidator.ValidateAndThrowException(currentBranch, "Base");
                _branchRepository.Update(currentBranch);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public DTO.Branch Get(GetBranchRequest request)
        {
            try
            {
                var branch = _branchRepository.FindBy(request.Id);
                branch.ThrowExceptionIfRecordIsNull();
                return _branchFactory.Execute(branch);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Delete(DeleteBranchRequest request)
        {
            try
            {
                var branch = _branchRepository.FindBy(request.Id);
                branch.ThrowExceptionIfRecordIsNull();
                var isReference = _branchRepository.IsReference(request.Id);
                if (isReference)
                    ExceptionExtensions.ThrowIsReferenceException();
                _branchRepository.Remove(branch);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse ChangeStatus(ChangeStatusRequest request)
        {
            try
            {
                var branch = _branchRepository.FindBy(request.Id);
                branch.ThrowExceptionIfRecordIsNull();
                if(branch.Status.Equals(request.Status))
                    ExceptionExtensions.ThrowStatusException(request.Status);
                branch.Status = request.Status;
                _branchRepository.Update(branch);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse IsReference(IsReferenceRequest request)
        {
            try
            {
                var isReference = _branchRepository.IsReference(request.Id);
                return new SuccessResponse { IsSuccess = isReference };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse AddDealer(RelationRequest request)
        {
            try
            {
                var branchDealer = TypeAdapter.Adapt<BranchDealer>(request);
                _branchDealerValidator.ValidateAndThrowException(branchDealer, "Base");
                _branchDealerRepository.Add(branchDealer);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse RemoveDealer(RelationRequest request)
        {
            try
            {
                var branchDealer = _branchDealerRepository.FindBy(branchDealerRe => branchDealerRe.BranchId == request.FirstReference && branchDealerRe.DealerId == request.SecondReference).FirstOrDefault();
                branchDealer.ThrowExceptionIfRecordIsNull();
                _branchDealerRepository.Remove(branchDealer);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}