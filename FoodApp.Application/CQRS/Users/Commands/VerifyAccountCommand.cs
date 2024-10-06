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
    public record VerifyAccountCommand(string Email, string OtpCode) : IRequest<ResultDTO<bool>>;

    public class VerifyAccountCommandHandler : IRequestHandler<VerifyAccountCommand, ResultDTO<bool>>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMediator _mediator;

        public VerifyAccountCommandHandler(IRepository<User> userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task<ResultDTO<bool>> Handle(VerifyAccountCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUserByEmailQuery(request.Email));
            var user = result.Data;

            if (!result.IsSuccess || user == null)
            {
                return ResultDTO<bool>.Faliure(ErrorCode.EmailIsNotFound, "Email not found.");
            }

            // Verify OTP
            if (user.OtpCode != request.OtpCode || user.OtpExpiry <= DateTime.UtcNow || user.OtpExpiry == null)
            {
                return ResultDTO<bool>.Faliure(ErrorCode.WrongOtp, "Invalid or expired OTP.");
            }

            user.IsActive = true;
            user.OtpCode = null;
            user.OtpExpiry = null;
            _userRepository.Update(user);
            _userRepository.SaveChanges();

            return ResultDTO<bool>.Sucess(true, "Account verified successfully.");
        }
    }
}
