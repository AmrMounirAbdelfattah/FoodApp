using FoodApp.Application.Common.DTOs;
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
    public record GetRecipeByIdQuery(int id) : IRequest<ResultDTO<Recipe>>;
    public class GetRecipeByIdQueryHandle : IRequestHandler<GetRecipeByIdQuery, ResultDTO<Recipe>>
    {
        private readonly IRepository<Recipe> _recipeRepository;
        public GetRecipeByIdQueryHandle(IRepository<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        public async Task<ResultDTO<Recipe>> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
        {
            var recipe = _recipeRepository.GetByID(request.id);
        
            if (recipe is null)
            {
                return ResultDTO<Recipe>.Faliure(ErrorCode.RecipeIsNotFound, "Invalid Recipe Id");
            }
            return ResultDTO<Recipe>.Sucess<Recipe>(recipe);
        }
    }
}
