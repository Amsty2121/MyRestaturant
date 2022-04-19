using Application.Common.Interfaces;
using Common.Models.PagedRequest;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Dishes.Queries.GetDishesPaged
{
    public class GetDishPagedQuery : IRequest<PaginatedResult<Dish>>
    {
        public PagedRequest PagedRequest { get; set; }
    }

    public class GetDishesPagedQueryHandler : IRequestHandler<GetDishPagedQuery, PaginatedResult<Dish>>
    {
        private readonly IGenericRepository<Dish> _dishRepository;



        public GetDishesPagedQueryHandler(IGenericRepository<Dish> dishRepository)
        {
            _dishRepository = dishRepository;
        }

        public async Task<PaginatedResult<Dish>> Handle(GetDishPagedQuery request, CancellationToken cancellationToken)
        {
            var a = await _dishRepository.GetPagedData<Dish>(request.PagedRequest);
            return a;
        }
    }
}