using FoodApp.Application.Common.Helpers;
using FoodApp.Domain.Entities;
using FoodApp.Domain.Interface.Base;
using MediatR;

namespace FoodApp.Application.CQRS.Categories.Commands
{
    public record AddCategoryCommand(AddCategoryDto Category) : IRequest<bool>;

    public record AddCategoryDto(string Name);

    public class AddCategoryCommandHandler(IRepository<Category> _repository) : IRequestHandler<AddCategoryCommand, bool>
    {
        public async Task<bool> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Category.Name))
                return false;

            var category = request.Category.MapOne<Category>();

            await Task.Run(() =>
            {
                _repository.Add(category);
                _repository.SaveChanges();
            });

            return true;
        }
    }
}
