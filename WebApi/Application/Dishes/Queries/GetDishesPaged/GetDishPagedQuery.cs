using System.Collections.Generic;
using Application.Common.Interfaces;
using Common.Models.PagedRequest;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Common.Dto.Ingredients;

namespace Application.Dishes.Queries.GetDishesPaged
{
    public class GetDishPagedDto
    {
        public int Id { get; set; }
        public string DishName { get; set; }
        public int DishPrice { get; set; }
        public string DishDescription { get; set; }
        public DishStatus DishStatus { get; set; }
        public DishCategory DishCategory { get; set; }

        public ICollection<GetIngredientDto> Ingredients { get; set; }
        /*public ICollection<Order> Orders { get; set; }*/
    }


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