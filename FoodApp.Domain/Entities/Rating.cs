using FoodApp.Domain.Enums;

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
