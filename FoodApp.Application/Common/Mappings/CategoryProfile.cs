﻿using AutoMapper;
using FoodApp.Application.Common.DTOs.Categories;
using FoodApp.Application.Common.ViewModels.Categories;
using FoodApp.Application.CQRS.Categories.Commands;
using FoodApp.Application.CQRS.Categories.Queries;
using FoodApp.Domain.Entities;

namespace FoodApp.Application.Common.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<AddCategoryViewModel, AddCategoryDto>();
            CreateMap<AddCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, CategoryViewModel>();
        }
    }
}
