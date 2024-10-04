namespace FoodApp.Domain.Entities
{
    public class Recipe:BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
       // [Precision(18, 2)]
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public ICollection<UserRecipes> UserRecipes { get; set; }
    }
}
