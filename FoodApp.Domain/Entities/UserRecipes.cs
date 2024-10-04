using FoodApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Domain.Entities
{
    public class UserRecipes:BaseModel
    {
        public int UserID { get; set; }
        public int RecipeID { get; set; }
        public User User { get; set; }
        public Recipe Recipe { get; set; }
        public Role Role { get; set; }
    }
}
