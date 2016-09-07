using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.IngredientGroups;
using FoodManager.DTO.Message.Ingredients;
using FoodManager.Infrastructure.Files;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Strings;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;
using ServiceStack.Common.Extensions;

namespace FoodManager.Services.Factories.Implements
{
    public class IngredientFactory : IIngredientFactory
    {
        private readonly IIngredientGroupRepository _ingredientGroupRepository;
        private readonly IStorageProvider _storageProvider;
        private readonly ISaucerConfigurationRepository _saucerConfigurationRepository;
        private readonly IReservationDetailRepository _reservationDetailRepository;

        public IngredientFactory(IIngredientGroupRepository ingredientGroupRepository, IStorageProvider storageProvider, ISaucerConfigurationRepository saucerConfigurationRepository, IReservationDetailRepository reservationDetailRepository)
        {
            _ingredientGroupRepository = ingredientGroupRepository;
            _storageProvider = storageProvider;
            _saucerConfigurationRepository = saucerConfigurationRepository;
            _reservationDetailRepository = reservationDetailRepository;
        }

        public IngredientResponse Execute(Ingredient ingredient)
        {
            return AppendProperties(new[] { ingredient }).FirstOrDefault();
        }

        public IEnumerable<IngredientResponse> Execute(IEnumerable<Ingredient> ingredients)
        {

            return AppendProperties(ingredients);
        }

        private IEnumerable<IngredientResponse> AppendProperties(IEnumerable<Ingredient> ingredients)
        {
            var ingredientsResponse = TypeAdapter.Adapt<List<IngredientResponse>>(ingredients);
            var ingredientGroups = _ingredientGroupRepository.FindBy(ingredientGroup => ingredientGroup.IsActive);
            var saucerConfigurations = _saucerConfigurationRepository.FindBy(saucerConfiguration => saucerConfiguration.IsActive);
            var reservationDetails = _reservationDetailRepository.FindBy(reservationDetail => reservationDetail.IsActive);

            ingredientsResponse.ForEach(ingredientResponse =>
            {
                var ingredient = ingredients.First(ingredientModel => ingredientModel.Id == ingredientResponse.Id);
                var ingredientGroup = ingredientGroups.First(ingredientGroupModel => ingredientGroupModel.Id == ingredient.IngredientGroupId);
                ingredientResponse.IngredientGroup = TypeAdapter.Adapt<IngredientGroupResponse>(ingredientGroup);
                var amountOfReferences = saucerConfigurations.Count(saucerConfiguration => saucerConfiguration.IngredientId == ingredient.Id);
                amountOfReferences += reservationDetails.Count(reservationDetail => reservationDetail.IngredientId == ingredient.Id);
                ingredientResponse.IsReference = amountOfReferences.IsNotZero();
            });

            return ingredientsResponse;
        }

        public List<Ingredient> FromCsv(string fileName)
        {
            var ingredients = new List<Ingredient>();
            var csvLines = _storageProvider.ReadAllLinesCsv(fileName);
            csvLines.ForEach(csvLine =>
            {
                var values = csvLine.Split(',');
                ingredients.Add(new Ingredient
                {
                    Name = values[0],
                    Energy = values[1].ToDecimal(),
                    Protein = values[2].ToDecimal(),
                    Carbohydrate = values[3].ToDecimal(),
                    Sugar = values[4].ToDecimal(),
                    Lipid = values[5].ToDecimal(),
                    Sodium = values[6].ToDecimal(),
                    Unit = values[7].ToInt(),
                    IngredientGroupId = values[8].ToInt(),
                    NetWeight = values[9].ToDecimal()
                });
            });

            return ingredients;
        }
    }
}