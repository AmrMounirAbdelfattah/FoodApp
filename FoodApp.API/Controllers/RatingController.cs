using FoodApp.Application.Common.Exceptions;
using FoodApp.Application.Common.Helpers;
using FoodApp.Application.Common.ViewModels;
using FoodApp.Application.Common.ViewModels.Ratings;
using FoodApp.Application.CQRS.Ratings.Commands;
using FoodApp.Application.CQRS.RecipesImages.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RatingController:ControllerBase
    {
        private readonly IMediator _mediator;

        public RatingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult> AddRatingToRecipe(AddRatingToRecipeViewModel viewModel)
        {

            var result = await _mediator.Send(viewModel.MapOne<AddRatingToRecipeCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel<int>.Sucess(result.Data, result.Message));
        }
    }
}
