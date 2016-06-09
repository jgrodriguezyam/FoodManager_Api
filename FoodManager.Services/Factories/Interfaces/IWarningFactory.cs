using System.Collections.Generic;
using FoodManager.DTO.Message.Warnings;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IWarningFactory
    {
        WarningResponse Execute(Warning warning);
        IEnumerable<WarningResponse> Execute(IEnumerable<Warning> warnings);
    }
}