using FoodApp.Application.Common.ViewModels.RecipeImages;
using FoodApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Common.ViewModels.Recipes
{
    public class RecipeDetailsViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<RecipeImagesViewModel> RecipeImages { get;set;}
        // [Precision(18, 2)]
        public decimal Price { get; set; }
        public string CategoryName{ get; set; }
        public string UserName{ get; set; }
    }
}
