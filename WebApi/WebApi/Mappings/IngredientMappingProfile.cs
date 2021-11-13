using Application.Ingredients.Queries.GetIngredientPaged;
using AutoMapper;
using Common.Dto.Ingredients;
using Common.Models.PagedRequest;
using Domain.Entities;

namespace WebApi.Mappings
{
    public class IngredientMappingProfile : Profile
    {
        public IngredientMappingProfile()
        {
            CreateMap<Ingredient, GetIngredientListDto>();
            CreateMap<Ingredient, GetIngredientDto>();
            CreateMap<Ingredient, InsertedIngredientDto>();
            CreateMap<Ingredient, InsertIngredientDto>();
            CreateMap<Ingredient, UpdatedIngredientDto>();
            CreateMap<Ingredient, UpdateIngredientDto>();

            CreateMap<Ingredient, GetIngredientPagedDto>();
            CreateMap<PaginatedResult<Ingredient>, PaginatedResult<GetIngredientPagedDto>>()
                .ForMember(x => x.Items, y => y.MapFrom(z => z.Items));

        }

    }
}
