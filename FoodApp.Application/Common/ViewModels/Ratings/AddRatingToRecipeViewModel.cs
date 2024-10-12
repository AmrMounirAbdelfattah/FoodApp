using FoodApp.Domain.Entities;
using FoodApp.Domain.Enums;

namespace FoodApp.Application.Common.ViewModels.Ratings
{
    public class AddRatingToRecipeViewModel
    {
        public int RecipeID { get; set; }
        public int UserID { get; set; }
        public RecipeRate Rate { get; set; }
        public string? Comment { get; set; }
    }
}
