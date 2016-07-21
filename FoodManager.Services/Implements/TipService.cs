using System.Collections.Generic;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Tips;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Infrastructure.Files;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.Tips;
using FoodManager.Services.Factories.Interfaces;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class TipService : ITipService
    {
        private readonly ITipQuery _tipQuery;
        private readonly ITipRepository _tipRepository;
        private readonly ITipValidator _tipValidator;
        private readonly ITipFactory _tipFactory;
        private readonly IStorageProvider _storageProvider;

        public TipService(ITipQuery tipQuery, ITipRepository tipRepository, ITipValidator tipValidator, ITipFactory tipFactory, IStorageProvider storageProvider)
        {
            _tipQuery = tipQuery;
            _tipRepository = tipRepository;
            _tipValidator = tipValidator;
            _tipFactory = tipFactory;
            _storageProvider = storageProvider;
        }

        public FindTipsResponse Find(FindTipsRequest request)
        {
            try
            {
                _tipQuery.WithOnlyActivated(true);
                _tipQuery.WithOnlyStatusActivated(request.OnlyStatusActivated);
                _tipQuery.WithOnlyStatusDeactivated(request.OnlyStatusDeactivated);
                _tipQuery.WithName(request.Name);
                _tipQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _tipQuery.TotalRecords();
                _tipQuery.Paginate(request.StartPage, request.EndPage);
                var tips = _tipQuery.Execute();

                return new FindTipsResponse
                {
                    Tips = TypeAdapter.Adapt<List<TipResponse>>(tips),
                    TotalRecords = totalRecords
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public CreateResponse Create(TipRequest request)
        {
            try
            {
                var tip = TypeAdapter.Adapt<Tip>(request);
                _tipValidator.ValidateAndThrowException(tip, "Base");
                _tipRepository.Add(tip);
                return new CreateResponse(tip.Id);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Update(TipRequest request)
        {
            try
            {
                var currentTip = _tipRepository.FindBy(request.Id);
                currentTip.ThrowExceptionIfRecordIsNull();
                var tipToCopy = TypeAdapter.Adapt<Tip>(request);
                TypeAdapter.Adapt(tipToCopy, currentTip);
                _tipValidator.ValidateAndThrowException(currentTip, "Base");
                _tipRepository.Update(currentTip);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public TipResponse Get(GetTipRequest request)
        {
            try
            {
                var tip = _tipRepository.FindBy(request.Id);
                tip.ThrowExceptionIfRecordIsNull();
                return TypeAdapter.Adapt<TipResponse>(tip);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Delete(DeleteTipRequest request)
        {
            try
            {
                var tip = _tipRepository.FindBy(request.Id);
                tip.ThrowExceptionIfRecordIsNull();
                _tipRepository.Remove(tip);
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
                var tip = _tipRepository.FindBy(request.Id);
                tip.ThrowExceptionIfRecordIsNull();
                tip.Status.ThrowExceptionIfIsSameStatus(request.Status);
                tip.Status = request.Status;
                _tipRepository.Update(tip);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Csv(CsvRequest request, File file)
        {
            try
            {
                var fileName = _storageProvider.Save(file);
                var tips = _tipFactory.FromCsv(fileName);
                tips.ForEach(tip => { _tipValidator.ValidateAndThrowException(tip, "Base"); });
                _tipRepository.AddAll(tips);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}