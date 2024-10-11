using FoodApp.Application.Common.Exceptions;
using FoodApp.Application.Common.ViewModels.Users;
using FoodApp.Application.Common.ViewModels;
using FoodApp.Application.CQRS.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FoodApp.Application.Common.ViewModels.Recipes;
using FoodApp.Application.CQRS.Recipes.Commands;
using FoodApp.Application.Common.Helpers;
using FoodApp.Application.CQRS.Recipes.Queries;
using static System.Net.Mime.MediaTypeNames;

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
            var result = await _mediator.Send(viewModel.MapOne<UpdateRecipeImageCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel<int>.Sucess(result.Data, result.Message));
            
        }
   
        [HttpGet]
        public async Task<IActionResult> GetRecipeById(int id)
        {
            var result = await _mediator.Send(new GetRecipeByIdQuery(id));
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            var recipeViewModel = result.Data.MapOne<RecipeDetailsViewModel>();
            return Ok(ResultViewModel<RecipeDetailsViewModel>.Sucess(recipeViewModel));
        }
    }
}
