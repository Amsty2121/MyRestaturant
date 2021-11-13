using System.Linq.Dynamic.Core;
using Application.Dishes.Queries.GetDishesPaged;
using Application.Orders.Queries.GetOrdersPaged;
using AutoMapper;
using Common.Dto.Orders;
using Common.Models.PagedRequest;
using Domain.Entities;

namespace WebApi.Mappings
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<OrdersWithStatusesTablesAndWaiters, GetOrderListDto>();
            CreateMap<Order, GetOrderDto>();
            
            CreateMap<OrderWithStatusTableAndWaiter, GetOrderDto>();
            CreateMap<Order, InsertedOrderDto>();
            CreateMap<Order, InsertOrderDto>();
            CreateMap<OrderUpdating, UpdatedOrderDto>();
            CreateMap<Order, UpdateOrderDto>();

            CreateMap<Order, GetOrderPagedDto>();
            CreateMap<PaginatedResult<Order>, PaginatedResult<GetOrderPagedDto>>()
                .ForMember(x => x.Items,
                    y => y.MapFrom(z => z.Items));

        }
    }
}