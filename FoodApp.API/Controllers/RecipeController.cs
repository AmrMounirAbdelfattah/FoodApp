using FoodApp.Application.Common.Helpers;
using FoodApp.Application.Common.ViewModels;
using FoodApp.Application.Common.ViewModels.Recipes;
using FoodApp.Application.CQRS.Recipes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RecipeController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ResultViewModel<IEnumerable<RecipeViewModel>>> GetAllRecipesAsync()
        {
            var result = await _mediator.Send(new GetAllRecipesQuery());

            var recipesVM = result.AsQueryable().Map<RecipeViewModel>().AsEnumerable();

            return ResultViewModel<IEnumerable<RecipeViewModel>>.Sucess(recipesVM, "Successfully Get All Recipes");
        }
    }
}
