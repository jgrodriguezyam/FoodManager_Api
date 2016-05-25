using System.Collections.Generic;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Menus;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.Menus;
using FoodManager.Services.Factories.Interfaces;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class MenuService : IMenuService
    {
        private readonly IMenuQuery _menuQuery;
        private readonly IMenuRepository _menuRepository;
        private readonly IMenuValidator _menuValidator;
        private readonly IMenuFactory _menuFactory;

        public MenuService(IMenuQuery menuQuery, IMenuRepository menuRepository, IMenuValidator menuValidator, IMenuFactory menuFactory)
        {
            _menuQuery = menuQuery;
            _menuRepository = menuRepository;
            _menuValidator = menuValidator;
            _menuFactory = menuFactory;
        }

        public FindMenusResponse Find(FindMenusRequest request)
        {
            try
            {
                _menuQuery.WithOnlyActivated(true);
                _menuQuery.WithOnlyStatusActivated(request.OnlyStatusActivated);
                _menuQuery.WithOnlyStatusDeactivated(request.OnlyStatusDeactivated);
                _menuQuery.WithDealer(request.DealerId);
                _menuQuery.WithSaucer(request.SaucerId);
                _menuQuery.WithOnlyToday(request.OnlyToday);
                _menuQuery.WithDaysWeek(request.DaysWeek);
                _menuQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _menuQuery.TotalRecords();
                _menuQuery.Paginate(request.StartPage, request.EndPage);
                var menus = _menuQuery.Execute();

                return new FindMenusResponse
                {
                    Menus = TypeAdapter.Adapt<List<MenuResponse>>(menus),
                    TotalRecords = totalRecords
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public CreateResponse Create(MenuRequest request)
        {
            try
            {
                var menu = TypeAdapter.Adapt<Menu>(request);
                _menuValidator.ValidateAndThrowException(menu, "Base,Create");
                _menuRepository.Add(menu);
                return new CreateResponse(menu.Id);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Update(MenuRequest request)
        {
            try
            {
                var currentMenu = _menuRepository.FindBy(request.Id);
                currentMenu.ThrowExceptionIfRecordIsNull();
                var menuToCopy = TypeAdapter.Adapt<Menu>(request);
                TypeAdapter.Adapt(menuToCopy, currentMenu);
                _menuValidator.ValidateAndThrowException(currentMenu, "Base,Update");
                _menuRepository.Update(currentMenu);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public DTO.Menu Get(GetMenuRequest request)
        {
            try
            {
                var menu = _menuRepository.FindBy(request.Id);
                menu.ThrowExceptionIfRecordIsNull();
                return _menuFactory.Execute(menu);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Delete(DeleteMenuRequest request)
        {
            try
            {
                var menu = _menuRepository.FindBy(request.Id);
                menu.ThrowExceptionIfRecordIsNull();
                _menuRepository.Remove(menu);
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
                var menu = _menuRepository.FindBy(request.Id);
                menu.ThrowExceptionIfRecordIsNull();
                menu.Status.ThrowExceptionIfIsSameStatus(request.Status);
                menu.Status = request.Status;
                _menuRepository.Update(menu);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}