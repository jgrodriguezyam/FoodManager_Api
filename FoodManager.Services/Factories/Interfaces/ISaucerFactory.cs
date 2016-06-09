using System.Collections.Generic;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface ISaucerFactory
    {
        List<Saucer> FromCsv(string fileName);
    }
}