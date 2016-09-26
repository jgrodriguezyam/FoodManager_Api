using FoodManager.Model;

namespace FoodManager.SoapService.Interfaces
{
    public interface IWorkerSoapRepository
    {
        void GetByBadge(string badge);
    }
}