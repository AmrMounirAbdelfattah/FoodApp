using FoodApp.Application.Common.DTOs;
using FoodApp.Domain.Entities;
using FoodApp.Domain.Enums;
using FoodApp.Domain.Interface.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.CQRS.Categories.Queries
{
    public record GetCategoryByIdQuery (int id ) :IRequest<ResultDTO<Category>>;

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, ResultDTO<Category>>
{
        private readonly IRepository<Category> _categoreRepository;

        public GetCategoryByIdQueryHandler(IRepository<Category> categoreRepository)
        {
            _categoreRepository = categoreRepository;
        }
        public async Task<ResultDTO<Category>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = _categoreRepository.GetByID(request.id);

            if (category is null)
            {
                return ResultDTO<Category>.Faliure(ErrorCode.UnKnown, "Invalid Recipe Id");
        }
            return ResultDTO<Category>.Sucess(category);



        }
    }
}
