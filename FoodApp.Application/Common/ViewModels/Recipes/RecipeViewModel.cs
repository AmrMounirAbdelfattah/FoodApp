namespace FoodApp.Application.Common.ViewModels.Recipes
{
    public record RecipeViewModel(string Name, string? Image, decimal Price, string Description, int Discount, string Category);
}
