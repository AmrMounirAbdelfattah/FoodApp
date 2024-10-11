using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Domain.Entities
{
    public class RecipeImages:BaseModel
    {
        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; }
        public string ImageUrl { get; set; }
    }
}
