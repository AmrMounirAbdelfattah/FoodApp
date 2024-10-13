using FoodApp.Application.Common.Helpers;
using FoodApp.Domain.Entities;
using FoodApp.Domain.Interface.Base;
using MediatR;

namespace FoodApp.Application.CQRS.Recipes.Commands
{
    public record AddRecipeCommand(AddRecipeDto Recipe) : IRequest<bool>;

    public record AddRecipeDto(string Name, string Description, string? Image, decimal Price, int Discount, int CategoryID);

    public class AddRecipeCommandHandler : IRequestHandler<AddRecipeCommand, bool>
    {
        private readonly IRepository<Recipe> _recipeRepository;
        private readonly IRepository<Category> _categoryRepository;

        public AddRecipeCommandHandler(IRepository<Recipe> recipeRepository, IRepository<Category> categoryRepository)
        {
            _recipeRepository = recipeRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(AddRecipeCommand request, CancellationToken cancellationToken)
        {
            var category = await Task.Run(() => _categoryRepository.GetByID(request.Recipe.CategoryID));
            if (category == null)
                return false; 

            var recipe = request.Recipe.MapOne<Recipe>();

            recipe.Category = category;

            await Task.Run(() =>
            {
                _recipeRepository.Add(recipe);
                _recipeRepository.SaveChanges();
            });

            return true;
        }
    }
}
