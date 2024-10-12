using AutoMapper;
using FoodApp.Application.Common.ViewModels.Ratings;
using FoodApp.Application.CQRS.Ratings.Commands;
using FoodApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Common.Mappings
{
    public class RatingProfile:Profile
    {
        public RatingProfile()
        {
            CreateMap<AddRatingToRecipeViewModel, AddRatingToRecipeCommand>();
            CreateMap<AddRatingToRecipeCommand,Rating >();
        }
    }
}
