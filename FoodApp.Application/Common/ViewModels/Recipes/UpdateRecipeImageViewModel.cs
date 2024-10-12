using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Common.ViewModels.Recipes
{
    public class UpdateRecipeImageViewModel
    {
        public ICollection<IFormFile> RecipeImages { get; set; }
        public int ID { get; set; }
        public int UserID { get; set; }
        public int RecipeID { get; set; }
    }
}
