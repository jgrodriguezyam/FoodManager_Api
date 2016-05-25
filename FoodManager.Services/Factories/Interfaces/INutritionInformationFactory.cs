using FoodManager.Infrastructure.Application;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface INutritionInformationFactory
    {
        NutritionInformation FindBySaucer(int saucerId);
    }
}