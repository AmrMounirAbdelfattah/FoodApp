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
    public record IsRecipeExistQuery(int id) : IRequest<ResultDTO<bool>>;
    public class IsRecipeExistQueryHandler : IRequestHandler<IsRecipeExistQuery, ResultDTO<bool>>
    {
        private readonly IRepository<Recipe> _recipeRepository;
        public IsRecipeExistQueryHandler(IRepository<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        public async Task<ResultDTO<bool>> Handle(IsRecipeExistQuery request, CancellationToken cancellationToken)
        {
            var result = _recipeRepository.Any(u => u.ID == request.id);
            if (result)
            {
                return ResultDTO<bool>.Sucess(true);
            }
            return ResultDTO<bool>.Faliure(ErrorCode.RecipeIsNotFound, "Recipe is Not Found");
        }
    }
}
