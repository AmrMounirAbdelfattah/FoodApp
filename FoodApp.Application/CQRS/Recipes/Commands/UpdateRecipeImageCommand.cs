using FoodApp.Application.Common.DTOs;
using FoodApp.Application.Common.Helpers;
using FoodApp.Application.CQRS.Recipes.Queries;
using FoodApp.Domain.Entities;
using FoodApp.Domain.Enums;
using FoodApp.Domain.Interface.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.CQRS.Recipes.Commands
{
    public record UpdateRecipeImageCommand(int ID,IEnumerable<IFormFile> RecipeImages,int UserID):IRequest<ResultDTO<bool>>;
    //public class UpdateRecipeImageCommandHandler : IRequestHandler<UpdateRecipeImageCommand, ResultDTO<bool>>
    //{

    //    private readonly IRepository<Recipe> _recipeRepository;
    //    private readonly CloudinaryService _cloudinaryService;
    //    private readonly IMediator _mediator;
    //    public UpdateRecipeImageCommandHandler(IRepository<Recipe> recipeRepository, CloudinaryService cloudinaryService, IMediator mediator)
    //    {
    //        _recipeRepository = recipeRepository;
    //        _cloudinaryService = cloudinaryService;
    //        _mediator = mediator;
    //    }
    ////    public async Task<ResultDTO<bool>> Handle(UpdateRecipeImageCommand request, CancellationToken cancellationToken)
    ////    {
    ////        var result = await _mediator.Send(new GetRecipeByIdQuery(request.ID));
    ////        if (!result.IsSuccess)
    ////        {
    ////            return ResultDTO<bool>.Faliure(result.ErrorCode, result.Message);
    ////        }
    ////        var recipe = result.Data;
    ////        if (recipe.UserID != request.UserID)
    ////        {
    ////            return ResultDTO<bool>.Faliure(ErrorCode.InvalidUserID, "Invalid User Id");
    ////        }
    ////        var newRecipeImages = request.RecipeImages;
    ////        //        if (request.RecipeImages != null && newRecipeImages.Count() > 0)
    ////        //       {
    ////        //    var imageUrls = new List<string>();
    ////        //    foreach (var image in newRecipeImages)
    ////        //    {
    ////        //        if (image.Length > 0)
    ////        //        {
    ////        //            var url = await  _cloudinaryService.UploadImageAsync(image);      //UploadImageAsync(image);
    ////        //            imageUrls.Add(url);
    ////        //        }
    ////        //    }
    ////        //    recipe.ImagesUrl = imageUrls;
    ////        //    _recipeRepository.Update(recipe);
    ////        //    _recipeRepository.SaveChanges();

    ////        //}
    ////        return ResultDTO<bool>.Sucess(true, "Recipe Images Updated Successfully");
    ////    }
    //}

}
