using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.IngredientGroups;
using FoodManager.DTO.Message.Ingredients;
using FoodManager.Infrastructure.Files;
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
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientFactory(IIngredientGroupRepository ingredientGroupRepository, IStorageProvider storageProvider, IIngredientRepository ingredientRepository)
        {
            _ingredientGroupRepository = ingredientGroupRepository;
            _storageProvider = storageProvider;
            _ingredientRepository = ingredientRepository;
        }

        public IngredientResponse Execute(Ingredient ingredient)
        {
            var ingredientResponse = TypeAdapter.Adapt<IngredientResponse>(ingredient);
            var ingredientGroup = _ingredientGroupRepository.FindBy(ingredient.IngredientGroupId);
            ingredientResponse.IngredientGroup = TypeAdapter.Adapt<IngredientGroupResponse>(ingredientGroup);
            ingredientResponse.IsReference = _ingredientRepository.IsReference(ingredient.Id);
            return ingredientResponse;
        }

        public IEnumerable<IngredientResponse> Execute(IEnumerable<Ingredient> ingredients)
        {
            return ingredients.Select(Execute);
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