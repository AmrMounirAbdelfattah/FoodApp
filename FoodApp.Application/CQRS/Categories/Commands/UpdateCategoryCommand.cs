using FoodApp.Application.Common.DTOs;
using FoodApp.Application.Common.DTOs.Categories;
using FoodApp.Application.Common.Helpers;
using FoodApp.Application.Common.ViewModels;
using FoodApp.Application.CQRS.Categories.Queries;
using FoodApp.Domain.Entities;
using FoodApp.Domain.Enums;
using FoodApp.Domain.Interface.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.CQRS.Categories.Commands
{
    public record UpdateCategoryCommand(int categoryId , string name):IRequest<ResultDTO<bool>>;

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ResultDTO<bool>>
{
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMediator _mediator;

        public UpdateCategoryCommandHandler(IRepository<Category> categoryRepository ,IMediator mediator)
        {
            _categoryRepository = categoryRepository;
            _mediator = mediator;
        }
        public async Task<ResultDTO<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var result  = await _mediator.Send(new GetCategoryByIdQuery(request.categoryId));

            if (!result.IsSuccess)
            {
                return ResultDTO<bool>.Faliure(ErrorCode.UnKnown,"NotFound");
            }
            var category = result.Data;

            category.Name = request.name;

               _categoryRepository.Update(category);
                _categoryRepository.SaveChanges();

            return ResultDTO<bool>.Sucess(true, "Category updated Successfully!");
        }
    }
}
