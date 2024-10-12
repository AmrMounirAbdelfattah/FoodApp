using FoodApp.Application.Common.DTOs;
using FoodApp.Domain.Entities;
using FoodApp.Domain.Enums;
using FoodApp.Domain.Interface.Base;
using MediatR;

namespace FoodApp.Application.CQRS.RecipesImages.Queries
{
    public record GetRecipeImageByIDQuery(int id) : IRequest<ResultDTO<RecipeImages>>;
    public class GetRecipeImageByIDQueryHandle : IRequestHandler<GetRecipeImageByIDQuery, ResultDTO<RecipeImages>>
    {
        private readonly IRepository<RecipeImages> _recipeImagesRepository;
        public GetRecipeImageByIDQueryHandle(IRepository<RecipeImages> recipeImagesRepository)
        {
            _recipeImagesRepository = recipeImagesRepository;
        }
        public async Task<ResultDTO<RecipeImages>> Handle(GetRecipeImageByIDQuery request, CancellationToken cancellationToken)
        {
            var recipe = _recipeImagesRepository.GetByID(request.id);

            if (recipe is null)
            {
                return ResultDTO<RecipeImages>.Faliure(ErrorCode.RecipeImageIsNotFound, "Invalid Image Id");
            }
            return ResultDTO<RecipeImages>.Sucess(recipe);
        }
    }
}
