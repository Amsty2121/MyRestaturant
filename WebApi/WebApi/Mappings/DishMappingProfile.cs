using Application.Dishes.Commands.UpdateDish;
using Application.Dishes.Queries.GetDishById;
using Application.Dishes.Queries.GetDishesList;
using AutoMapper;
using Common.Dto.Dishes;
using Domain.Entities;

namespace WebApi.Mappings
{
    public class DishMappingProfile : Profile
    {
        public DishMappingProfile()
        {
            CreateMap<DishesWithStatusesAndCategories, GetDishListDto>();
            CreateMap<Dish, GetDishDto>();
            CreateMap<DishWithStatusAndCategory,GetDishDto > ();
            CreateMap<Dish, InsertedDishDto>();
            CreateMap<Dish, InsertDishDto>();
            CreateMap<DishUpdating, UpdatedDishDto>();
            CreateMap<Dish, UpdateDishDto>();
        }

    }
}