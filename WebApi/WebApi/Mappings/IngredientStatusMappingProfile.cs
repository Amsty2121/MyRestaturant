using AutoMapper;
using Common.Dto.IngredientStatuses;
using Domain.Entities;

namespace WebApi.Mappings
{
    public class IngredientStatusMappingProfile : Profile
    {
        public IngredientStatusMappingProfile()
        {
            CreateMap<IngredientStatus, GetIngredientStatusesListDto>();
            CreateMap<IngredientStatus, GetIngredientStatusDto>();
            CreateMap<IngredientStatus, InsertedIngredientStatusDto>();
        }
    }
}
