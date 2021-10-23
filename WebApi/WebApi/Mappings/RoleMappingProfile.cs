using AutoMapper;
using Common.Dto.Roles;
using Domain.Entities.Authorization;

namespace WebApi.Mappings
{
	public class RoleMappingProfile : Profile
	{
		public RoleMappingProfile()
		{
			CreateMap<Role, GetRolesListDto>().ForMember(x => x.RoleName, y => y.MapFrom(z => z.Name));
		}
	}
}
