using FoodApp.Application.Common.DTOs;
using FoodApp.Application.CQRS.RecipesImages.Queries;
using FoodApp.Domain.Entities;
using FoodApp.Domain.Interface.Base;
using MediatR;

namespace FoodApp.Application.CQRS.RecipesImages.Commands
{
    public record UpdateRecipeImageCommand(int ID, int RecipeID, string recipeImageUrl) : IRequest<ResultDTO<bool>>;
    public class UpdateRecipeImageCommandHandler : IRequestHandler<UpdateRecipeImageCommand, ResultDTO<bool>>
    {

        private readonly IRepository<RecipeImages> _recipeImagesRepository;
        private readonly IMediator _mediator;
        public UpdateRecipeImageCommandHandler(IRepository<RecipeImages> recipeImagesRepository, IMediator mediator)
        {
            _recipeImagesRepository = recipeImagesRepository;
            _mediator = mediator;
        }
        public async Task<ResultDTO<bool>> Handle(UpdateRecipeImageCommand request, CancellationToken cancellationToken)
        {

            var result = await _mediator.Send(new GetRecipeImageByIDQuery(request.ID));
            if (!result.IsSuccess)
            {
                return ResultDTO<bool>.Faliure(result.ErrorCode, result.Message);
            }
            var recipeImage = result.Data;
           // _recipeImagesRepository.Delete(request.ID);
            recipeImage.ImageUrl = request.recipeImageUrl;
            _recipeImagesRepository.Update(recipeImage);
            _recipeImagesRepository.SaveChanges();
            return ResultDTO<bool>.Sucess(true, "Recipe Images Updated Successfully");
        }
    }

}
