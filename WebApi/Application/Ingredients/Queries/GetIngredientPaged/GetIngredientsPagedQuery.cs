using Application.Common.Interfaces;
using Common.Models.PagedRequest;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Ingredients.Queries.GetIngredientPaged
{
	public class GetIngredientsPagedQuery : IRequest<PaginatedResult<Ingredient>>
	{
		public PagedRequest PagedRequest { get; set; }
	}

	public class GetIngredientsPagedQueryHandler : IRequestHandler<GetIngredientsPagedQuery, PaginatedResult<Ingredient>>
	{
		private readonly IGenericRepository<Ingredient> _ingredientRepository;



		public GetIngredientsPagedQueryHandler(IGenericRepository<Ingredient> ingredientRepository)
		{
			_ingredientRepository = ingredientRepository;
		}

		public async Task<PaginatedResult<Ingredient>> Handle(GetIngredientsPagedQuery request, CancellationToken cancellationToken)
		{
			return await _ingredientRepository.GetPagedData<Ingredient>(request.PagedRequest);
        }
	}
}
