using System.Linq;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Dealers;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.Dealers;
using FoodManager.Services.Factories.Interfaces;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class DealerService : IDealerService
    {
        private readonly IDealerQuery _dealerQuery;
        private readonly IDealerRepository _dealerRepository;
        private readonly IDealerValidator _dealerValidator;
        private readonly IDealerFactory _dealerFactory;

        public DealerService(IDealerQuery dealerQuery, IDealerRepository dealerRepository, IDealerValidator dealerValidator, IDealerFactory dealerFactory)
        {
            _dealerQuery = dealerQuery;
            _dealerRepository = dealerRepository;
            _dealerValidator = dealerValidator;
            _dealerFactory = dealerFactory;
        }

        public FindDealersResponse Find(FindDealersRequest request)
        {
            try
            {
                _dealerQuery.WithOnlyActivated(true);
                _dealerQuery.WithOnlyStatusActivated(request.OnlyStatusActivated);
                _dealerQuery.WithOnlyStatusDeactivated(request.OnlyStatusDeactivated);
                _dealerQuery.WithName(request.Name);
                _dealerQuery.WithBranch(request.BranchId);
                _dealerQuery.WithoutBranch(request.WithoutBranchId);
                _dealerQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _dealerQuery.TotalRecords();
                _dealerQuery.Paginate(request.StartPage, request.EndPage);
                var dealers = _dealerQuery.Execute();

                return new FindDealersResponse
                {
                    Dealers = _dealerFactory.Execute(dealers).ToList(),
                    TotalRecords = totalRecords
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public CreateResponse Create(DealerRequest request)
        {
            try
            {
                var dealer = TypeAdapter.Adapt<Dealer>(request);
                _dealerValidator.ValidateAndThrowException(dealer, "Base");
                _dealerRepository.Add(dealer);
                return new CreateResponse(dealer.Id);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Update(DealerRequest request)
        {
            try
            {
                var currentDealer = _dealerRepository.FindBy(request.Id);
                currentDealer.ThrowExceptionIfRecordIsNull();
                var dealerToCopy = TypeAdapter.Adapt<Dealer>(request);
                TypeAdapter.Adapt(dealerToCopy, currentDealer);
                _dealerValidator.ValidateAndThrowException(currentDealer, "Base");
                _dealerRepository.Update(currentDealer);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public DealerResponse Get(GetDealerRequest request)
        {
            try
            {
                var dealer = _dealerRepository.FindBy(request.Id);
                dealer.ThrowExceptionIfRecordIsNull();
                return _dealerFactory.Execute(dealer);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Delete(DeleteDealerRequest request)
        {
            try
            {
                var dealer = _dealerRepository.FindBy(request.Id);
                dealer.ThrowExceptionIfRecordIsNull();
                var isReference = _dealerRepository.IsReference(request.Id);
                isReference.ThrowExceptionIfIsReference();
                _dealerRepository.Remove(dealer);
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
                var dealer = _dealerRepository.FindBy(request.Id);
                dealer.ThrowExceptionIfRecordIsNull();
                dealer.Status.ThrowExceptionIfIsSameStatus(request.Status);
                dealer.Status = request.Status;
                _dealerRepository.Update(dealer);
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
                var isReference = _dealerRepository.IsReference(request.Id);
                return new SuccessResponse { IsSuccess = isReference };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}