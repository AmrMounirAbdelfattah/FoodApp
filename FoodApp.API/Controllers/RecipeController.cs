using FoodApp.Application.Common.Exceptions;
using FoodApp.Application.Common.Helpers;
using FoodApp.Application.Common.ViewModels;
using FoodApp.Application.Common.ViewModels.Recipes;
using FoodApp.Application.CQRS.Recipes.Commands;
using FoodApp.Application.CQRS.Recipes.Queries;
using FoodApp.Application.CQRS.RecipesImages.Commands;
using FoodApp.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RecipeController(IMediator _mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<ResultViewModel<bool>> AddRecipeAsync([FromBody] AddRecipeDto recipeDto)
        {
            if (recipeDto == null)
            {
                return ResultViewModel<bool>.Faliure(ErrorCode.UnKnown, "Invalid recipe data");
            }

            var result = await _mediator.Send(new AddRecipeCommand(recipeDto));

            if (result)
            {
                return ResultViewModel<bool>.Sucess(result, "Recipe added successfully");
            }
            else
            {
                return ResultViewModel<bool>.Faliure(ErrorCode.EmptyCategoryName, "Failed to add recipe. Category not found.");
            }
        }
        [HttpGet]
        public async Task<ResultViewModel<IEnumerable<RecipeViewModel>>> GetAllRecipesAsync()
        {
            var result = await _mediator.Send(new GetAllRecipesQuery());

            var recipesVM = result.AsQueryable().Map<RecipeViewModel>().AsEnumerable();

            return ResultViewModel<IEnumerable<RecipeViewModel>>.Sucess(recipesVM, "Successfully Get All Recipes");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var result = await _mediator.Send(new DeleteRecipeCommand(id));

            return Ok(result);

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


