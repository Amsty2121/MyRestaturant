using Common.Dto.Admins;
using Domain.Entities;
using Domain.Entities.Authorization;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Admins.Commands.RegisterAdmin
{
    public class RegisterAdminCommand : IRequest<IdentityResult>
    {
        public InsertAdminDto Dto { get; set; }
        public UserManager<User> UserManager { get; set; }
    }
    public class RegisterAdminCommandHandler : IRequestHandler<RegisterAdminCommand, IdentityResult>
    {
        public async Task<IdentityResult> Handle(RegisterAdminCommand request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                UserName = request.Dto.Username,
                UserDetails = new UserDetails()
                {
                    FirstName = request.Dto.FirstName,
                    LastName = request.Dto.LastName,
                }
            };
            var userInsertion = await request.UserManager.CreateAsync(user, request.Dto.Password);

            if (userInsertion.Succeeded)
            {
                var adminInsertion = await request.UserManager.AddToRoleAsync(user, "admin");

                if (adminInsertion.Succeeded)
                {
                    return IdentityResult.Success;
                }

            }
            return IdentityResult.Failed();
        }
    }
}