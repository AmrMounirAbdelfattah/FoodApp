using FoodApp.Application.Common.DTOs;
using FoodApp.Application.Common.Helpers;
using FoodApp.Application.CQRS.Recipes.Queries;
using FoodApp.Application.CQRS.Users.Queries;
using FoodApp.Domain.Entities;
using FoodApp.Domain.Enums;
using FoodApp.Domain.Interface.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.CQRS.Ratings.Commands
{
    public record AddRatingToRecipeCommand(int RecipeID, int UserID, RecipeRate Rate, string? Comment) :IRequest<ResultDTO<bool>>;
    public class AddRatingTorecipeCommandHandler : IRequestHandler<AddRatingToRecipeCommand, ResultDTO<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Rating> _ratingRepository;
        public AddRatingTorecipeCommandHandler(IMediator mediator,IRepository<Rating> ratingRepository)
        {
            _mediator = mediator;
           _ratingRepository = ratingRepository;
        }
        public async Task<ResultDTO<bool>> Handle(AddRatingToRecipeCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new IsRecipeExistQuery(request.RecipeID));
            if (!result.IsSuccess)
            {
                return ResultDTO<bool>.Faliure(result.ErrorCode, result.Message);
            }
           result = await _mediator.Send(new IsUserExistQuery(request.UserID));
            if (!result.IsSuccess)
            {
                return ResultDTO<bool>.Faliure(result.ErrorCode, result.Message);
            }
            result = await _mediator.Send(new IsRecipeHasRatingQuery( request.RecipeID,request.UserID));
            if (!result.IsSuccess)
            {
                return ResultDTO<bool>.Faliure(result.ErrorCode, result.Message);
            }
            //
            var rating = request.MapOne<Rating>();
            rating.UserID = request.UserID;
            rating.RecipeID = request.RecipeID;
            rating.Rate=    request.Rate;
            rating.Comment = request.Comment;
            _ratingRepository.Add(rating);
            _ratingRepository.SaveChanges();
            return ResultDTO<bool>.Sucess(true,"Rating Added Successfully");
        }
    }

}
