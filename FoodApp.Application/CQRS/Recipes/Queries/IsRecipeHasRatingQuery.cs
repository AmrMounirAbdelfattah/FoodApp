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

namespace FoodApp.Application.CQRS.Recipes.Queries
{
    public record IsRecipeHasRatingQuery(int RecipeID,int UserID) : IRequest<ResultDTO<bool>>;
    public class IsRecipeHasRatingQueryHandler : IRequestHandler<IsRecipeHasRatingQuery, ResultDTO<bool>>
    {
        private readonly IRepository<Rating> _ratingRepository;
        public IsRecipeHasRatingQueryHandler(IRepository<Rating> ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }
        public async Task<ResultDTO<bool>> Handle(IsRecipeHasRatingQuery request, CancellationToken cancellationToken)
        {
            var result = _ratingRepository.Any(u => u.RecipeID == request.RecipeID && u.UserID==request.UserID);
            if (!result)
            {
                return ResultDTO<bool>.Sucess(true);
            }
            return ResultDTO<bool>.Faliure(ErrorCode.RecipeHasBeenRated, "You Can Rate Recipe One Time");
        }
    }
}
