using AutoMapper;
using FoodApp.Application.Common.ViewModels.RecipeImages;
using FoodApp.Application.Common.ViewModels.Recipes;
using FoodApp.Application.CQRS.RecipesImages.Commands;
using FoodApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Common.Mappings
{
    public class RecipeImagesProfile:Profile
    {
        public RecipeImagesProfile()
        {
           CreateMap<UpdateRecipeImageViewModel, UpdateRangeOfRecipeImagesCommand>();
        }
    }
}
