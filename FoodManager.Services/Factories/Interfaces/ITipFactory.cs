using System.Collections.Generic;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface ITipFactory
    {
        List<Tip> FromCsv(string fileName);
    }
}