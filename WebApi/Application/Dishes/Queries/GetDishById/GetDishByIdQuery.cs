﻿using System.Collections.Generic;
using Application.Common.Interfaces;
using Common.Dto.Dishes;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Dishes.Queries.GetDishById
{
    public class GetDishByIdQuery : IRequest<GetDishDto>
    {
        public int DishId { get; set; }
    }

    class GetDishByIdQueryHandler : IRequestHandler<GetDishByIdQuery, GetDishDto>
    {
        private readonly IGenericRepository<Dish> _dishRepository;
        private readonly IGenericRepository<Ingredient> _ingredientRepository;
        private readonly IGenericRepository<DishStatus> _dishStatusRepository;
        private readonly IGenericRepository<DishCategory> _dishCategoryRepository;
        public GetDishByIdQueryHandler(IGenericRepository<Dish> dishRepository,
                                         IGenericRepository<Ingredient> ingredientRepository,
                                         IGenericRepository<DishStatus> dishStatusRepository,
                                         IGenericRepository<DishCategory> dishCategoryRepository)
        {
            _dishRepository = dishRepository;
            _ingredientRepository = ingredientRepository;
            _dishStatusRepository = dishStatusRepository;
            _dishCategoryRepository = dishCategoryRepository;
        }
        public async Task<GetDishDto> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
        {
            Dish dish = await _dishRepository.GetByIdWithInclude(request.DishId, x => x.Ingredients);
            if (dish == null)
            {
                throw new EntityDoesNotExistException("The Dish does not exist");
            }

            var dishStatus = await _dishStatusRepository.GetById(dish.DishStatusId);
            var dishCategory = await _dishCategoryRepository.GetById(dish.DishCategoryId);

            var getDishDto = new GetDishDto()
            {
                Id = dish.Id,
                DishDescription = dish.DishDescription,
                DishName = dish.DishName,
                DishPrice = dish.DishPrice,
                DishCategoryId = dish.DishCategoryId,
                DishStatusId = dish.DishStatusId,
                DishCategoryName = dishCategory.DishCategoryName,
                DishStatusName = dishStatus.DishStatusName,
                IngredientsId = dish.Ingredients.Select(x=>x.Id).ToList()
            };

            return getDishDto;
        }
    }
}