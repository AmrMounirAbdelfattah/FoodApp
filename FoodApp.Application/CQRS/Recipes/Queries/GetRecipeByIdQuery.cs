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
    public record GetRecipeByIdQuery(int id):IRequest<ResultDTO<RecipeDetailsDTO>>;
    public record RecipeDetailsDTO (string Name, string Description,
        ICollection<RecipeImagesViewModel> RecipeImages , 
        decimal Price, 
        string CategoryName,
        string UserName);
    public class GetRecipeByIdQueryHandle : IRequestHandler<GetRecipeByIdQuery, ResultDTO<RecipeDetailsDTO>>
    {
        private readonly IRepository<Recipe> _recipeRepository;
        public GetRecipeByIdQueryHandle(IRepository<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        public async Task<ResultDTO<RecipeDetailsDTO>> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
        {
            // var recipe = _recipeRepository.GetByID(request.id);
            var recipe =  _recipeRepository.GetByIDWithProjection(r => r.ID == request.id,
                recipe => new RecipeDetailsDTO
                (
                     recipe.Name,
                   recipe.Description,
                     recipe.RecipeImages.Select(img => new RecipeImagesViewModel
                    {
                        ImageUrl = img.ImageUrl
                    }).ToList(), 
                   recipe.Price,
                    recipe.Category.Name,
                     recipe.User.UserName
                ));
            if (recipe is null)
            {
                return ResultDTO<RecipeDetailsDTO>.Faliure(ErrorCode.RecipeIsNotFound,"Invalid Recipe Id");
            }
                return ResultDTO<RecipeDetailsDTO>.Sucess<RecipeDetailsDTO>(recipe);
        }
    }
}
