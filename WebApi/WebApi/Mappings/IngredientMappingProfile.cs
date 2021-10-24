﻿using AutoMapper;
using Common.Dto.Ingredients;
using Domain.Entities;

namespace WebApi.Mappings
{
    public class IngredientMappingProfile : Profile
    {
        public IngredientMappingProfile()
        {
            CreateMap<IngredientsWithStatuses, GetIngredientListDto>();
            CreateMap<IngredientWithStatus, GetIngredientDto>();
            CreateMap<Ingredient, GetIngredientDto>();
            CreateMap<Ingredient, InsertedIngredientDto>();
            CreateMap<Ingredient, InsertIngredientDto>();
            CreateMap<IngredientStatusUpdating, UpdatedIngredientDto>();
            CreateMap<Ingredient, UpdateIngredientDto>();
        }
        
    }
}
