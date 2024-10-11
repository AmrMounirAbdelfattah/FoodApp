using FoodApp.Application.Common.DTOs;
using FoodApp.Application.CQRS.Recipes.Queries;
using FoodApp.Domain.Entities;
using FoodApp.Domain.Enums;
using FoodApp.Domain.Interface.Base;
using MediatR;

namespace FoodApp.Application.CQRS.Recipes.Commands
{
 
    public record UpdateRecipeCommand(int ID,string Name, string Description, decimal Price, int CategoryID, int UserID) :IRequest<ResultDTO<bool>>;
    //public class UpdateRecipeCommandHandler : IRequestHandler<UpdateRecipeCommand, ResultDTO<bool>>
    //{
    //    private readonly  IMediator _mediator;
    //    private readonly IRepository<Recipe> _recipeRepository;
    //    public UpdateRecipeCommandHandler(IMediator mediator, IRepository<Recipe> recipeRepository)
    //    {
    //        _mediator = mediator;
    //        _recipeRepository = recipeRepository;
    //    }
    //    public async Task<ResultDTO<bool>> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    //    {
    //        var result = await _mediator.Send(new GetRecipeByIdQuery(request.ID));
    //        if (!result.IsSuccess)
    //        {
    //            return ResultDTO<bool>.Faliure(result.ErrorCode, result.Message);
    //        }
    //        var recipe = result.Data;
    //        if(recipe.UserID != request.UserID)
    //        {
    //            return ResultDTO<bool>.Faliure(ErrorCode.InvalidUserID,  "Invalid User Id");
    //        }
    //        recipe.Name = request.Name;
    //        recipe.Description = request.Description;
    //        recipe.Price = request.Price;
    //        recipe.CategoryID = request.CategoryID;
    //        _recipeRepository.Update(recipe);
    //        _recipeRepository.SaveChanges();

    //        return ResultDTO<bool>.Sucess(true, "Recipe Updated Successfully");
    //    }
    //}


}
