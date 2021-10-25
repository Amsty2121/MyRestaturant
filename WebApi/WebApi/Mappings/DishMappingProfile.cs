using AutoMapper;
using Common.Dto.Dishes;
using Common.Dto.Ingredients;
using Common.Models.PagedRequest;
using Domain.Entities;

namespace WebApi.Mappings
{
    public class DishMappingProfile : Profile
    {
        public DishMappingProfile()
        {
            CreateMap<Dish, GetDishDto>();
            CreateMap<Dish, GetDishListDto>();

            CreateMap<Dish, InsertDishDto>();
            CreateMap<Dish, InsertedDishDto>();

            CreateMap<Dish, UpdateDishDto>();
            CreateMap<Dish, UpdatedDishDto>();

            CreateMap<Dish, GetDishPagedDto>();
            CreateMap<PaginatedResult<Dish>, PaginatedResult<GetDishPagedDto>>()
                .ForMember(x => x.Items, y => y.MapFrom(z => z.Items));
        }

    }
}