using AutoMapper;
using Common.Dto.OrderStatuses;
using Domain.Entities;

namespace WebApi.Mappings
{
    public class OrderStatusMappingProfile : Profile
    {
        public OrderStatusMappingProfile()
        {
            CreateMap<OrderStatus, GetOrderStatusesListDto>();
            CreateMap<OrderStatus, GetOrderStatusDto>();
            CreateMap<OrderStatus, InsertedOrderStatusDto>();
        }
    }
}