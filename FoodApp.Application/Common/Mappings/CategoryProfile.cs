using AutoMapper;
using FoodApp.Application.Common.ViewModels.Categories;
using FoodApp.Application.CQRS.Categories.Commands;
using FoodApp.Domain.Entities;

namespace FoodApp.Application.Common.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<AddCategoryViewModel, AddCategoryDto>();
            CreateMap<AddCategoryDto, Category>();
        }
    }
}
