using System.Collections.Generic;
using System.Linq;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Waiters.Queries.GetWaiterById
{
    public class WaiterWithTablesAndOrders
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<int> TablesId { get; set; }
        public ICollection<int> OrdersId { get; set; }
    }

    public class GetWaiterByIdQuery : IRequest<WaiterWithTablesAndOrders>
	{
		public int WaiterId { get; set; }
	}

	public class GetWaiterByIdQueryHandler : IRequestHandler<GetWaiterByIdQuery, WaiterWithTablesAndOrders>
	{
		private readonly IGenericRepository<Waiter> _waiterRepository;
        private readonly IGenericRepository<Table> _tableRepository;
        private readonly IGenericRepository<Order> _orderRepository;

		public GetWaiterByIdQueryHandler(IGenericRepository<Waiter> waiterRepository,
			                             IGenericRepository<Table> tableRepository,
                                         IGenericRepository<Order> orderRepository)
		{
			_waiterRepository = waiterRepository;
            _tableRepository = tableRepository;
            _orderRepository = orderRepository;
		}

        public async Task<WaiterWithTablesAndOrders> Handle(GetWaiterByIdQuery request,
            CancellationToken cancellationToken)
        {
            Waiter waiter = await _waiterRepository.GetByIdWithInclude(request.WaiterId, x => x.UserDetails,x=>x.Tables);
            if (waiter == null)
            {
                throw new EntityDoesNotExistException("The Waiter does not exist");
            }

            var tables = (await _tableRepository.GetWhere(x => x.WaiterId == waiter.Id)).ToList();

            List<Order> ordersList = new List<Order>();

            foreach (var table in tables)
            {
                var orders = (await _orderRepository.GetWhere(x => x.TableId == table.Id));
                if (orders != null)
                {
                    foreach (var order in orders)
                    {
                        ordersList.Add(order);
                    }
                }
            }

            var waiterWithTablesAndOrders = new WaiterWithTablesAndOrders()
            {
                Id = waiter.Id,
                FirstName = waiter.UserDetails.FirstName,
                LastName = waiter.UserDetails.LastName,
                TablesId = tables.Select(x => x.Id).Distinct().ToList(),
                OrdersId = ordersList.Select(x => x.Id).Distinct().ToList()
            };

            return waiterWithTablesAndOrders;
        }
    }
}
