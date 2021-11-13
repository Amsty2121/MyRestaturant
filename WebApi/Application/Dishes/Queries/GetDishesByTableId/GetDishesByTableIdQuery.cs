using System.Collections.Generic;
using System.Linq;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Common.Dto.Dishes;

namespace Application.Dishes.Queries.GetDishesByTableId
{
    public class GetDishesByTableIdQuery : IRequest<ICollection<GetDishDto>>
    {
        public int TableId { get; set; }
    }

    public class GetDishByIdQueryHandler : IRequestHandler<GetDishesByTableIdQuery, ICollection<GetDishDto>>
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Dish> _dishRepository;
        private readonly IGenericRepository<Table> _tableRepository;

        public GetDishByIdQueryHandler(IGenericRepository<Order> orderRepository,
            IGenericRepository<Dish> dishRepository,
            IGenericRepository<Table> tableRepository)
        {
            _orderRepository = orderRepository;
            _dishRepository = dishRepository;
            _tableRepository = tableRepository;
        }

        public async Task<ICollection<GetDishDto>> Handle(GetDishesByTableIdQuery request,
            CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetWhere(x => x.TableId == request.TableId);

            ICollection<GetDishDto> result = new List<GetDishDto>();
            foreach (var order in orders)
            {
                var ord = await _orderRepository.GetByIdWithInclude(order.Id, x => x.Dish);
                var dish = ord.Dish;
                var d = await _dishRepository.GetByIdWithInclude(dish.Id, x => x.DishStatus, x => x.DishCategory,x=>x.DishIngredients);
                var mappedDish = new GetDishDto()
                {
                    Id = d.Id,
                    DishName = d.DishName,
                    DishDescription = d.DishDescription,
                    DishCategoryId = d.DishCategoryId,
                    DishCategoryName = d.DishCategory.DishCategoryName,
                    DishPrice = d.DishPrice,
                    DishStatusId = d.DishStatusId,
                    DishStatusName = d.DishStatus.DishStatusName,
                    IngredientsId = d.DishIngredients.Select(x=>x.IngredientId).ToList()
                };
                result.Add(mappedDish);
            }

            return result;
        }
    }
}