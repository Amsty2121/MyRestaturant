using Application.Dishes.Queries.GetDishesPaged;
using AutoMapper;
using Common.Dto.Dishes;
using Common.Models.PagedRequest;
using Domain.Entities;

namespace WebApi.Mappings
{
    public class DishMappingProfile : Profile
    {
        public DishMappingProfile()
        {
            CreateMap<Dish, GetDishListDto>();
            CreateMap<Dish, GetDishDto>();

            CreateMap<Dish, InsertedDishDto>();
            CreateMap<Dish, InsertDishDto>();

            CreateMap<Dish, UpdatedDishDto>();
            CreateMap<Dish, UpdateDishDto>();

            CreateMap<Dish, GetDishPagedDto>();
            CreateMap<PaginatedResult<Dish>, PaginatedResult<GetDishPagedDto>>()
                .ForMember(x => x.Items, y => y.MapFrom(z => z.Items));

        }

    }
}