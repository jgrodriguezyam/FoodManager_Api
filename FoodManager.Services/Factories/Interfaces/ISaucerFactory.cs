using System.Collections.Generic;
using FoodManager.DTO.Message.Saucers;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface ISaucerFactory
    {
        SaucerResponse Execute(Saucer saucer);
        IEnumerable<SaucerResponse> Execute(IEnumerable<Saucer> saucers);
        List<Saucer> FromCsv(string fileName);
    }
}