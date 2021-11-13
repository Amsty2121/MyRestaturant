using Application.Kitcheners.Commands.AssignKitchenerToDIshOrder;
using Application.Kitcheners.Queries.GetKitchenerById;
using AutoMapper;
using Common.Dto.Kitcheners;
using Domain.Entities;

namespace WebApi.Mappings
{
    public class KitchenerMappingProfile : Profile
    {
        public KitchenerMappingProfile()
        {
			CreateMap<Kitchener, GetKitchenerListDto>()
                .ForMember(x => x.FirstName, y =>
                    y.MapFrom(s => s.UserDetails.FirstName))
                .ForMember(x => x.LastName, y =>
                    y.MapFrom(s => s.UserDetails.LastName))
                .ForMember(x => x.Id, y =>
                    y.MapFrom(s => s.Id));

            CreateMap<Kitchener, GetKitchenerDto>()
                .ForMember(x => x.FirstName, y =>
                    y.MapFrom(s => s.UserDetails.FirstName))
                .ForMember(x => x.LastName, y =>
                    y.MapFrom(s => s.UserDetails.LastName))
                .ForMember(x => x.Id, y =>
                    y.MapFrom(s => s.Id));

            CreateMap<Kitchener, InsertedKitchenerDto>()
                .ForMember(x => x.FirstName, y =>
                    y.MapFrom(s => s.UserDetails.FirstName))
                .ForMember(x => x.LastName, y =>
                    y.MapFrom(s => s.UserDetails.LastName))
                .ForMember(x => x.Id, y =>
                    y.MapFrom(s => s.Id));

            CreateMap<GetKitchenerDto, KitchenerWithOrders>();
            CreateMap<KitchenerWithOrders,GetKitchenerDto >();
            CreateMap<AssignedKitchenerToDIshOrderDto, AssignedKitchenerToDishOrder> (); 
            CreateMap<AssignedKitchenerToDishOrder, AssignedKitchenerToDIshOrderDto > ();
        }
    }
}
