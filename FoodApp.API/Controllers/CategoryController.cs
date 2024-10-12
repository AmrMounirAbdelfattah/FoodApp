using FoodApp.Application.Common.Helpers;
using FoodApp.Application.Common.ViewModels;
using FoodApp.Application.Common.ViewModels.Categories;
using FoodApp.Application.CQRS.Categories.Commands;
using FoodApp.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CategoryController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ResultViewModel<bool>> AddCategoryAsync(AddCategoryViewModel viewModel)
        {
            var result = await _mediator.Send(new AddCategoryCommand(viewModel.MapOne<AddCategoryDto>()));

            if (!result)
                return ResultViewModel<bool>.Faliure(ErrorCode.EmptyCategoryName, "Failed to add category");

            return ResultViewModel<bool>.Sucess(result, "Category added successfully");
        }

        [HttpDelete("{id}")]
        public async Task<ResultViewModel<bool>> DeleteCategoryAsync(int id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand(id));

            if (!result)
                return ResultViewModel<bool>.Faliure(ErrorCode.UnKnown, "Failed to delete category or category not found");

            return ResultViewModel<bool>.Sucess(result, "Category deleted successfully");
        }
    }
}
