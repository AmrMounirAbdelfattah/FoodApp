using FoodApp.Application.Common.DTOs;
using FoodApp.Application.Common.Helpers;
using FoodApp.Application.CQRS.Users.Queries;
using FoodApp.Domain.Entities;
using FoodApp.Domain.Enums;
using FoodApp.Domain.Interface.Base;
using MediatR;

namespace FoodApp.Application.CQRS.Users.Commands
{
    public record UpdateUserCommand(UpdateUserDto UserDto) : IRequest<ResultDTO<int>>;

    public record UpdateUserDto(string UserName, string Password, string ConfirmPassword,
        string Email, string Phone, string Country);

    public class UpdateUserCommandHandler(IRepository<User> _repository, IMediator _mediator) : IRequestHandler<UpdateUserCommand, ResultDTO<int>>
    {
        public async Task<ResultDTO<int>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var valid = await Validate(request);
            if (!valid.IsSuccess)
                return valid;

            var user = await _mediator.Send(new GetUserByEmailQuery(request.UserDto.Email));

            user.Data.Password = BCrypt.Net.BCrypt.HashPassword(request.UserDto.Password);
            var otpCode = OTPGenerator.GenerateOTP();
            user.Data.OtpCode = otpCode;
            user.Data.OtpExpiry = DateTime.UtcNow.AddMinutes(90);
            _repository.Update(user.Data);
            _repository.SaveChanges();
            EmailService.SendEmail(user.Data.Email, "OTP", otpCode);
            return ResultDTO<int>.Sucess(user.Data.ID, "User updated successfully");
        }

        private async Task<ResultDTO<int>> Validate(UpdateUserCommand request)
        {
            var result = await _mediator.Send(new IsEmailExistQuery(request.UserDto.Email));
            if (!result.IsSuccess)
                return ResultDTO<int>.Faliure(ErrorCode.EmailIsNotFound, "Email is Not Found");

            result = await _mediator.Send(new IsUserNameExistQuery(request.UserDto.UserName));
            if (!result.IsSuccess)
                return ResultDTO<int>.Faliure(ErrorCode.UserNameIsNotFound, "User Name is Not Found");

            if (request.UserDto.Password != request.UserDto.ConfirmPassword)
                return ResultDTO<int>.Faliure(ErrorCode.PasswordsDontMatch, "Passwords don't match");

            return ResultDTO<int>.Sucess(1, "");
        }
    }
}
