using FoodApp.Application.Common.DTOs;
using FoodApp.Application.Common.Helpers;
using FoodApp.Application.CQRS.Users.Queries;
using FoodApp.Domain.Entities;
using FoodApp.Domain.Enums;
using FoodApp.Domain.Interface.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.CQRS.Users.Commands
{
    public record LoginUserCommand(UserLoginDto userLoginDto) : IRequest<ResultDTO<string>>;

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ResultDTO<string>>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMediator _mediator;

        public LoginUserCommandHandler(IRepository<User> userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }
        public async Task<ResultDTO<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.First(c => c.Email == request.userLoginDto.Email);

            if (user is null || !BCrypt.Net.BCrypt.Verify(request.userLoginDto.Password, user.Password))
            {
                return ResultDTO<string>.Faliure(ErrorCode.WrongPasswordOrEmail, "Email or Password is incorrect");
            }
            var userDTO = user.MapOne<UserDto>();
            var token = TokenGenerator.GenerateToken(userDTO);

            return  ResultDTO<int>.Sucess(token, "User Login Successfully!");
        }
    }
}
