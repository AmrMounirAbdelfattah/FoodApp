using FoodApp.Domain.Entities;
using FoodApp.Domain.Interface.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.CQRS.Categories.Commands
{
    public record DeleteCategoryCommand(int CategoryID) : IRequest<bool>;

    public class DeleteCategoryCommandHandler(IRepository<Category> _repository) : IRequestHandler<DeleteCategoryCommand, bool>
    {
        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _repository.GetByID(request.CategoryID);
            if (category == null)
                return false;

            category.Deleted = true;

            await Task.Run(() =>
            {
                _repository.Update(category);
                _repository.SaveChanges();
            });

            return true;
        }
    }
}
