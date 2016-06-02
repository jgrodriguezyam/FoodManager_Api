using System;
using System.Collections.Generic;
using FastMapper;
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

        public IngredientFactory(IIngredientGroupRepository ingredientGroupRepository, IStorageProvider storageProvider)
        {
            _ingredientGroupRepository = ingredientGroupRepository;
            _storageProvider = storageProvider;
        }

        public DTO.Ingredient Execute(Ingredient ingredient)
        {
            var ingredientDto = TypeAdapter.Adapt<DTO.Ingredient>(ingredient);
            var ingredientGroup = _ingredientGroupRepository.FindBy(ingredient.IngredientGroupId);
            ingredientDto.IngredientGroup = TypeAdapter.Adapt<DTO.IngredientGroup>(ingredientGroup);
            return ingredientDto;
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
                    Amount = values[1].ToInt(),
                    Energy = values[2].ToDecimal(),
                    Protein = values[3].ToDecimal(),
                    Carbohydrate = values[4].ToDecimal(),
                    Sugar = values[5].ToDecimal(),
                    Lipid = values[6].ToDecimal(),
                    Sodium = values[7].ToDecimal(),
                    Unit = values[8].ToInt(),
                    IngredientGroupId = values[9].ToInt()
                });
            });

            return ingredients;
        }
    }
}