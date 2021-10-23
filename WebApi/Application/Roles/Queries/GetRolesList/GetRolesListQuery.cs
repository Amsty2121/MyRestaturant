using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Authorization;

namespace Application.Roles.Queries.GetRolesList
{
	public class GetRolesListQuery : IRequest<IEnumerable<Role>>
	{
		public RoleManager<Role> RoleManager { get; set; }
	}

	public class GetRolesListQueryHandler : IRequestHandler<GetRolesListQuery, IEnumerable<Role>>
	{
		public async Task<IEnumerable<Role>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
		{
			var roles = request.RoleManager.Roles.AsEnumerable();

			return roles;
		}
	}
}
