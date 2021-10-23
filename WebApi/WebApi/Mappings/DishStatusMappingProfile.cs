using AutoMapper;
using Common.Dto.DishStatuses;
using Domain.Entities;

namespace WebApi.Mappings
{
    public class DishStatusMappingProfile : Profile
    {
        public DishStatusMappingProfile()
        {
            CreateMap<DishStatus, GetDishStatusesListDto>();
            CreateMap<DishStatus, GetDishStatusDto>();
            CreateMap<DishStatus, InsertedDishStatusDto>();
        }
    }
}