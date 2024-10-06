using FoodApp.Application.Common.DTOs;
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
    public record ChangePasswordCommand(string Email, string OldPassword, string NewPassword, string ConfirmNewPassword) : IRequest<ResultDTO<bool>>;

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ResultDTO<bool>>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMediator _mediator;

        public ChangePasswordCommandHandler(IRepository<User> userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task<ResultDTO<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUserByEmailQuery(request.Email));
            var user = result.Data;

            if (!result.IsSuccess || user == null)
            {
                return ResultDTO<bool>.Faliure(ErrorCode.EmailIsNotFound, "Email not found.");
            }

            // Verify the old password
            if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, user.Password))
            {
                return ResultDTO<bool>.Faliure(ErrorCode.WrongPasswordOrEmail, "Incorrect old password.");
            }

            // Check if new passwords match
            if (request.NewPassword != request.ConfirmNewPassword)
            {
                return ResultDTO<bool>.Faliure(ErrorCode.PasswordsDontMatch, "New passwords do not match.");
            }

            // Hash the new password and update it
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            _userRepository.Update(user);
            _userRepository.SaveChanges();

            return ResultDTO<bool>.Sucess(true, "Password changed successfully.");
        }
    }
}
