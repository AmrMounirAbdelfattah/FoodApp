﻿using FoodApp.Application.Common.Helpers;
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
                return BadRequest();
            }
            return Ok(ResultViewModel<int>.Sucess(result.Data, result.Message));
        }
   
     


    }
}

