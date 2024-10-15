using FoodApp.Application.Common.DTOs;
using FoodApp.Application.Common.Exceptions;
using FoodApp.Application.Common.Helpers;
using FoodApp.Application.Common.ViewModels;
using FoodApp.Application.Common.ViewModels.Categories;
using FoodApp.Application.CQRS.Categories.Commands;
using FoodApp.Application.CQRS.Categories.Queries;
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

        [HttpPut]
        public async Task<ResultDTO<bool>> UpdateCategory(int categoryId, string name)
        {
            var result = await _mediator.Send(new UpdateCategoryCommand(categoryId, name));

            return ResultDTO<bool>.Sucess(true, "Category updated successfully");

        }
        [HttpGet]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery(id));
            if (!result.IsSuccess)

                throw new BusinessException(result.ErrorCode, result.Message);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ResultViewModel<IEnumerable<CategoryViewModel>>> GetAllCategoriesAsync()
        {
            var result = await _mediator.Send(new GetAllCategoriesQuery());

            var categoriesVM = result.AsQueryable().Map<CategoryViewModel>().AsEnumerable();

            return ResultViewModel<IEnumerable<CategoryViewModel>>.Sucess(categoriesVM, "Successfully Get All Categories");
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
