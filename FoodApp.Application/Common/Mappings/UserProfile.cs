using AutoMapper;
using FoodApp.Application.Common.DTOs;
using FoodApp.Application.Common.ViewModels.Users;
using FoodApp.Application.CQRS.Users.Commands;
using FoodApp.Domain.Entities;

namespace FoodApp.Application.Common.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserViewModel, RegisterUserCommand>();
            CreateMap<RegisterUserCommand, User>();
            CreateMap<ResetPasswordViewModel, ResetPasswordCommand>();
            CreateMap<User,UserDto>().ReverseMap();
        }
    }

    
}
