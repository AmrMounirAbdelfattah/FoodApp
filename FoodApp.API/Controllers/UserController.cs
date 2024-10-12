using FoodApp.Application.Common.Exceptions;
using FoodApp.Application.Common.Helpers;
using FoodApp.Application.Common.ViewModels;
using FoodApp.Application.Common.ViewModels.Users;
using FoodApp.Application.CQRS.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel viewModel)
        {
            var result = await _mediator.Send(viewModel.MapOne<RegisterUserCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode,result.Message);
            }
            return Ok(ResultViewModel<int>.Sucess(result.Data, result.Message));
        }

        [HttpPut]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
        {
            var result = await _mediator.Send(viewModel.MapOne<ResetPasswordCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel<int>.Sucess(result.Data, result.Message));
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPasswordAsync(string email)
        {
            var result = await _mediator.Send(new ForgetPasswordCommand(email));
            if (!result.IsSuccess)
                return BadRequest("User not found or invalid request.");

            return Ok(ResultViewModel<string>.Sucess(result, "Password reset link has been sent."));
        }

        [HttpPost]
        public async Task<IActionResult> VerifyAccount(VerifyAccountViewModel viewModel)
        {
            var result = await _mediator.Send(new VerifyAccountCommand(viewModel.Email, viewModel.OtpCode));

            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }

            return Ok(ResultViewModel<bool>.Sucess(result.Data, "Account verified successfully."));
        }

        [HttpPut]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            var result = await _mediator.Send(new ChangePasswordCommand(viewModel.Email, viewModel.OldPassword, viewModel.NewPassword, viewModel.ConfirmNewPassword));

            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }

            return Ok(ResultViewModel<bool>.Sucess(result.Data, "Password changed successfully."));
        }

    }
}

