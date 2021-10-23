using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Kitcheners.Queries.GetKitchenersList
{
	public class GetKitchenersListQuery : IRequest<IEnumerable<Kitchener>>
	{
	}

	public class GetKitchenerListQueryHandler : IRequestHandler<GetKitchenersListQuery, IEnumerable<Kitchener>>
	{
		private readonly IGenericRepository<Kitchener> _kitchenerRepository;

		public GetKitchenerListQueryHandler(IGenericRepository<Kitchener> kitchenerRepository)
		{
			_kitchenerRepository = kitchenerRepository; 
		}
		public async Task<IEnumerable<Kitchener>> Handle(GetKitchenersListQuery request, CancellationToken cancellationToken)
		{
			IEnumerable<Kitchener> kitcheners = await _kitchenerRepository.GetAllWithInclude(x => x.UserDetails);

			return kitcheners;
		}
	}
}
