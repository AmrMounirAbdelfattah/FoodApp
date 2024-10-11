namespace FoodApp.Domain.Entities
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
