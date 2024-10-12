using FoodApp.Application.Common.Exceptions;
using FoodApp.Application.Common.Helpers;
using FoodApp.Application.Common.ViewModels;
using FoodApp.Application.Common.ViewModels.Recipes;
using FoodApp.Application.CQRS.Recipes.Commands;
using FoodApp.Application.CQRS.Recipes.Queries;
using FoodApp.Application.CQRS.RecipesImages.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RecipeController:ControllerBase
    {
        private readonly IMediator _mediator;

        public RecipeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRecipe(UpdateRecipeViewModel viewModel)
        {
            var result = await _mediator.Send(viewModel.MapOne<UpdateRecipeCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel<int>.Sucess(result.Data, result.Message));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRecipeImage(UpdateRecipeImageViewModel viewModel)
        {
            var result = await _mediator.Send(viewModel.MapOne<UpdateRangeOfRecipeImagesCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel<int>.Sucess(result.Data, result.Message));
            
        }
   
        [HttpGet]
        public async Task<IActionResult> GetRecipeById(int id)
        {
            var result = await _mediator.Send(new GetRecipeDetailsByIdQuery(id));
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            var recipeViewModel = result.Data.MapOne<RecipeDetailsViewModel>();
            return Ok(ResultViewModel<RecipeDetailsViewModel>.Sucess(recipeViewModel));
        }
    }
}
