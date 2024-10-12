using FoodApp.Application.Common.DTOs;
using FoodApp.Application.Common.ViewModels.RecipeImages;
using FoodApp.Application.Common.ViewModels.Recipes;
using FoodApp.Domain.Entities;
using FoodApp.Domain.Enums;
using FoodApp.Domain.Interface.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.CQRS.Recipes.Queries
{
    public record GetRecipeDetailsByIdQuery(int id):IRequest<ResultDTO<RecipeDetailsDto>>;
   
    public class RecipeDetailsDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<RecipeImagesViewModel> RecipeImages { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public string UserName { get; set; }
        public  int UserID { get; set; }
    }
    public class GetRecipeDetailsByIdQueryHandle : IRequestHandler<GetRecipeDetailsByIdQuery, ResultDTO<RecipeDetailsDto>>
    {
        private readonly IRepository<Recipe> _recipeRepository;
        public GetRecipeDetailsByIdQueryHandle(IRepository<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        public async Task<ResultDTO<RecipeDetailsDto>> Handle(GetRecipeDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var recipe =  _recipeRepository.GetByIDWithProjection(r => r.ID == request.id,
                recipe => new RecipeDetailsDto
                {   Name =recipe.Name,
                    Description= recipe.Description,
                    RecipeImages=   recipe.RecipeImages.Select(img => new RecipeImagesViewModel
                    {
                        ImageUrl = img.ImageUrl
                    }).ToList(),
                    Price= recipe.Price,
                   CategoryName= recipe.Category.Name,
                    UserName=recipe.User.UserName,
                    UserID = recipe.UserID
                
                }
                    
                );
            if (recipe is null)
            {
                return ResultDTO<RecipeDetailsDto>.Faliure(ErrorCode.RecipeIsNotFound,"Invalid Recipe Id");
            }
                return ResultDTO<RecipeDetailsDto>.Sucess<RecipeDetailsDto>(recipe);
        }
    }
}
