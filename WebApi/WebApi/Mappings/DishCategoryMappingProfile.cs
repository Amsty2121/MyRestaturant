using AutoMapper;
using Common.Dto.DishCategories;
using Common.Dto.Dishes;
using Domain.Entities;

namespace WebApi.Mappings
{
    public class DishCategoryMappingProfile : Profile
    {
        public DishCategoryMappingProfile()
        {
            CreateMap<DishCategory, GetDishCategoriesListDto>();
            CreateMap<DishCategory, GetDishCategoryDto>();
            CreateMap<DishCategory, InsertedDishCategoryDto>();
            CreateMap<DishCategory, InsertDishCategoryDto>();
            CreateMap<DishCategory, UpdatedDishCategoryDto>();
            CreateMap<DishCategory, UpdateDishCategoryDto>();
        }

    }
}