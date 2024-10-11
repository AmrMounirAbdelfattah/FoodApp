using AutoMapper;
using FoodApp.Application.Common.ViewModels.Recipes;
using FoodApp.Application.CQRS.Recipes.Queries;
using FoodApp.Domain.Entities;

namespace FoodApp.Application.Common.Mappings
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<Recipe, RecipeDto>();
            CreateMap<RecipeDto, RecipeViewModel>();
        }
    }
}
