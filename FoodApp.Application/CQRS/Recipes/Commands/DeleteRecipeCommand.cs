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

namespace FoodApp.Application.CQRS.Recipes.Commands
{
    public record DeleteRecipeCommand(int id) : IRequest<ResultDTO<bool>>;

    public class DeleteRecipeCommandHandler : IRequestHandler<DeleteRecipeCommand, ResultDTO<bool>>
    {
        private readonly IRepository<Recipe> _recipeRepository;
        private readonly IMediator _mediator;

        public DeleteRecipeCommandHandler(IRepository<Recipe> recipeRepository, IMediator mediator)
        {
            _recipeRepository = recipeRepository;
            _mediator = mediator;
        }
        public async Task<ResultDTO<bool>> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = _recipeRepository.GetByID(request.id);

            if (recipe == null)
            {
                return ResultDTO<bool>.Faliure(ErrorCode.UnKnown, "not found");
            }
                _recipeRepository.Delete(recipe);
                _recipeRepository.SaveChanges();

            return ResultDTO<bool>.Sucess(true, "Recipe Deleted Successfully");





        }


    }
    }

