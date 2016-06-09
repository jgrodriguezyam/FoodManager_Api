using System.Collections.Generic;
using FoodManager.DTO.Message.Menus;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IMenuFactory
    {
        MenuResponse Execute(Menu menu);
        IEnumerable<MenuResponse> Execute(IEnumerable<Menu> menus);
    }
}