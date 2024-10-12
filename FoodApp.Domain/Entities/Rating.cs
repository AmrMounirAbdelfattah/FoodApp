using FoodApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Domain.Entities
{
    public class Rating :BaseModel
    {
        public Recipe Recipe { get; set; }
        public int RecipeID { get; set; }
        public User User { get; set; }
        public int UserID { get; set; }
        public RecipeRate Rate { get; set; }
        public string Comment { get; set; }
    }
}
