using Application.Orders.Commands.UpdateOrder;
using Application.Orders.Queries.GetOrderById;
using Application.Orders.Queries.GetOrdersList;
using AutoMapper;
using Common.Dto.Orders;
using Domain.Entities;

namespace WebApi.Mappings
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<OrdersWithStatusesTablesAndWaiters, GetOrderListDto>();
            CreateMap<Order, GetOrderDto>();
            CreateMap<OrdersWithStatusesTablesAndWaiters, GetOrderDto>();
            CreateMap<Order, InsertedOrderDto>();
            CreateMap<Order, InsertOrderDto>();
            CreateMap<OrderUpdating, UpdatedOrderDto>();
            CreateMap<Order, UpdateOrderDto>(); 
        }
    }
}