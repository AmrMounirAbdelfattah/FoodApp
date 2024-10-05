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
    public record GetUserByEmailQuery(string email) : IRequest<ResultDTO<User>>;
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, ResultDTO<User>>
    {
        private readonly IRepository<User> _userRepository;
        public GetUserByEmailQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ResultDTO<User>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = _userRepository.First(u => u.Email == request.email);
            if (user is null)
            {
                return ResultDTO<User>.Faliure(ErrorCode.EmailIsNotFound, "Email is Not Found");
               
            }
            return ResultDTO<User>.Sucess(user);
        }


    }
}
