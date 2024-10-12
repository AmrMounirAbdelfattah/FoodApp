using CloudinaryDotNet;
using FoodApp.Application.Common.DTOs;
using FoodApp.Application.Common.Helpers.CloudinaryHelper;
using FoodApp.Application.Common.Helpers.ImageHelper;
using FoodApp.Application.CQRS.Recipes.Queries;
using FoodApp.Domain.Entities;
using FoodApp.Domain.Enums;
using FoodApp.Domain.Interface.Base;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FoodApp.Application.CQRS.RecipesImages.Commands
{
    public record UpdateRangeOfRecipeImagesCommand(int ID, int RecipeID, IEnumerable<IFormFile> RecipeImages, int UserID) : IRequest<ResultDTO<bool>>;
    public class UpdateRangeOfRecipeImagesCommandHandler : IRequestHandler<UpdateRangeOfRecipeImagesCommand, ResultDTO<bool>>
    {
        private readonly IRepository<RecipeImages> _recipeImagesRepository;
        private readonly IMediator _mediator;
        private readonly IImageService _imageService;
        public UpdateRangeOfRecipeImagesCommandHandler(IRepository<RecipeImages> recipeImagesRepository, IMediator mediator, IImageService imageService)
        {
            _recipeImagesRepository = recipeImagesRepository;
            _mediator = mediator;
            _imageService = imageService;
        }

        public async Task<ResultDTO<bool>> Handle(UpdateRangeOfRecipeImagesCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetRecipeByIdQuery(request.RecipeID));
            if (!result.IsSuccess)
            {
                return ResultDTO<bool>.Faliure(result.ErrorCode, result.Message);
            }
            var recipe = result.Data;
            if (recipe.UserID != request.UserID)
            {
                return ResultDTO<bool>.Faliure(ErrorCode.InvalidUserID, "Invalid User Id");
            }
            var newRecipeImages = request.RecipeImages;
            var imageUrls = await _imageService.ConfigureImages(newRecipeImages);
                foreach (var recipeImageUrl in imageUrls)
                {
                    _mediator.Send(new UpdateRecipeImageCommand(request.ID, request.RecipeID, recipeImageUrl));
                }
            return ResultDTO<bool>.Sucess<bool>(true);
        }

    }
}
