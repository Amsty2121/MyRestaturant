using Application.Waiters.Queries.GetWaiterById;
using AutoMapper;
using Common.Dto.Waiters;
using Domain.Entities;

namespace WebApi.Mappings
{
    public class WaiterMappingProfile : Profile
    {
        public WaiterMappingProfile()
        {
            CreateMap<Waiter, GetWaiterListDto>()
                .ForMember(x => x.FirstName, y =>
                    y.MapFrom(s => s.UserDetails.FirstName))
                .ForMember(x => x.LastName, y =>
                    y.MapFrom(s => s.UserDetails.LastName))
                .ForMember(x => x.Id, y =>
                    y.MapFrom(s => s.Id));

            CreateMap<Waiter, GetWaiterDto>()
                .ForMember(x => x.FirstName, y =>
                    y.MapFrom(s => s.UserDetails.FirstName))
                .ForMember(x => x.LastName, y =>
                    y.MapFrom(s => s.UserDetails.LastName))
                .ForMember(x => x.Id, y =>
                    y.MapFrom(s => s.Id));

            CreateMap<Waiter, InsertedWaiterDto>()
                .ForMember(x => x.FirstName, y =>
                    y.MapFrom(s => s.UserDetails.FirstName))
                .ForMember(x => x.LastName, y =>
                    y.MapFrom(s => s.UserDetails.LastName))
                .ForMember(x => x.Id, y =>
                    y.MapFrom(s => s.Id));

            CreateMap<WaiterWithTablesAndOrders, GetWaiterDto>();
        }
    }
}
