using FoodApp.Application.Common.Helpers;
using FoodApp.Domain.Entities;
using FoodApp.Domain.Interface.Base;
using MediatR;

namespace FoodApp.Application.CQRS.Categories.Queries
{
    public record GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>;

    public record CategoryDto(int ID, string Name);

    public class GetAllCategoriesQueryHandler(IRepository<Category> _repository) : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
    {
        public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await Task.Run(() => _repository.GetAll());

            var categoriesDto = categories.Map<CategoryDto>().AsEnumerable();

            return categoriesDto;
        }
    }
}
