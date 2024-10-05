using FoodApp.Application.Common.DTOs;
using FoodApp.Application.Common.Exceptions;
using FoodApp.Application.CQRS.Users.Queries;
using FoodApp.Domain.Entities;
using FoodApp.Domain.Enums;
using FoodApp.Domain.Interface.Base;
using MediatR;

namespace FoodApp.Application.CQRS.Users.Commands
{
    public record ResetPasswordCommand(string NewPassword,string ConfirmNewPassword,string Email,string OtpCode) :IRequest<ResultDTO<bool>>;
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResultDTO<bool>>
    {
        private IMediator _mediator;
        private IRepository<User> _userRepository;
        public ResetPasswordCommandHandler(IMediator mediator, IRepository<User> userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }
        public  async Task<ResultDTO<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUserByEmailQuery(request.Email));
            var user = result.Data;
            if (!result.IsSuccess)
                {
                return ResultDTO<bool>.Faliure(ErrorCode.EmailIsNotFound, "Email is Not Found");
                }
               if(user.OtpCode!=request.OtpCode || user.OtpExpiry <= DateTime.Now || user.OtpExpiry==null)
            {
                return ResultDTO<bool>.Faliure(ErrorCode.WrongOtp, "Wrong Otp");
            }
            if (request.NewPassword != request.ConfirmNewPassword)
            {
                return ResultDTO<bool>.Faliure(ErrorCode.PasswordsDontMatch, "Passwords don't match");
            }
            var newHashedPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            user.Password = newHashedPassword;
            user.OtpCode = null;
            user.OtpExpiry = null;
            _userRepository.Update(user);
            _userRepository.SaveChanges();
            return ResultDTO<bool>.Sucess(true);
        }
    }
}
