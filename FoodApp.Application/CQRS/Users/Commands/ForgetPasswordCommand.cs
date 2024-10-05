using FoodApp.Application.Common.DTOs;
using FoodApp.Application.CQRS.Users.Queries;
using FoodApp.Domain.Entities;
using FoodApp.Domain.Interface.Base;
using MediatR;

namespace FoodApp.Application.CQRS.Users.Commands
{
    public record ForgetPasswordCommand(string Email) : IRequest<ResultDTO<bool>>;

    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, ResultDTO<bool>>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMediator _mediator;

        public ForgetPasswordCommandHandler(IRepository<User> userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task<ResultDTO<bool>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new IsEmailExistQuery(request.Email));
            if (!result.IsSuccess)
                return result;

            //var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            //var resetUrl = $"https://yourapp.com/reset-password?token={token}&email={request.Email}";
            //await _emailSender.SendEmailAsync(request.Email, "Password Reset", $"Click to reset your password: {resetUrl}");

            return ResultDTO<bool>.Sucess(true);
        }
    }
}
