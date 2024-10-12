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

namespace FoodApp.Application.CQRS.Users.Queries
{
    public record IsUserExistQuery(int id) : IRequest<ResultDTO<bool>>;
    public class IsUserExistQueryHandler : IRequestHandler<IsUserExistQuery, ResultDTO<bool>>
    {
        private readonly IRepository<User> _userRepository;
        public IsUserExistQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ResultDTO<bool>> Handle(IsUserExistQuery request, CancellationToken cancellationToken)
        {
            var result = _userRepository.Any(u => u.ID == request.id);
            if (result)
            {
                return ResultDTO<bool>.Sucess(true);
            }
            return ResultDTO<bool>.Faliure(ErrorCode.UserIsNotFound, "User is Not Found");
        }
    }
}
