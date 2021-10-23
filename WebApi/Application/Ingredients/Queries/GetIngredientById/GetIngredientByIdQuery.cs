﻿using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Ingredients.Queries.GetIngredientById
{
    public class IngredientWithStatus
    {
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public string IngredientDescription { get; set; }
        public int IngredientStatusId { get; set; }
        public string IngredientStatusName { get; set; }
    }

    public class GetIngredientByIdQuery : IRequest<IngredientWithStatus>
    {
        public int IngredientId { get; set; }
    }

    public class GetIngredientByIdQueryHandler : IRequestHandler<GetIngredientByIdQuery, IngredientWithStatus>
    {
        private readonly IGenericRepository<Ingredient> _ingredientRepository;
        private readonly IGenericRepository<IngredientStatus> _ingredientStatusRepository;

        public GetIngredientByIdQueryHandler(IGenericRepository<Ingredient> ingredientRepository, IGenericRepository<IngredientStatus> ingredientStatusRepository)
        {
            _ingredientRepository = ingredientRepository;
            _ingredientStatusRepository = ingredientStatusRepository;
        }

        public async Task<IngredientWithStatus> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
        {
            Ingredient ingredient = await _ingredientRepository.GetByIdWithInclude(request.IngredientId, x => x.IngredientStatus);

            if (ingredient == null)
            {
                throw new EntityDoesNotExistException("The Ingredient does not exist");
            }

            IngredientStatus ingredientStatus = await _ingredientStatusRepository.GetById(ingredient.IngredientStatusId);

            IngredientWithStatus ingredientWithStatus = new IngredientWithStatus()
            {
                Id = ingredient.Id,
                IngredientName = ingredient.IngredientName,
                IngredientDescription = ingredient.IngredientDescription,
                IngredientStatusId = ingredient.IngredientStatusId,
                IngredientStatusName = ingredientStatus.IngredientStatusName
            };

            return ingredientWithStatus;
        }
    }
}