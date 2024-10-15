using FoodApp.Application.Common.Helpers;
using FoodApp.Domain.Entities;
using FoodApp.Domain.Interface.Base;
using MediatR;

namespace FoodApp.Application.CQRS.Recipes.Queries
{
    public record GetAllRecipesQuery : IRequest<IEnumerable<RecipeDto>>;

    public record RecipeDto(string Name, string? Image, decimal Price, string Description, int Discount, string Category);

    public class GetAllRecipesQueryHandler(IRepository<Recipe> _repository) : IRequestHandler<GetAllRecipesQuery, IEnumerable<RecipeDto>>
    {
        public async Task<IEnumerable<RecipeDto>> Handle(GetAllRecipesQuery request, CancellationToken cancellationToken)
        {
            var recipes = await Task.Run(() => _repository.GetAll());

            var recipesDto = recipes.Map<RecipeDto>().AsEnumerable();

            return recipesDto;
        }
    }
}
