
using Domain.Entities;
using Domain.Entities.Authorization;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using Common.Dto.Waiters;

namespace Application.Waiters.Commands.RegisterWaiter
{
    public class RegisterWaiterCommand : IRequest<IdentityResult>
    {
        public InsertWaiterDto Dto { get; set; }
        public UserManager<User> UserManager { get; set; }
    }

    public class RegisterWaiterCommandHandler : IRequestHandler<RegisterWaiterCommand, IdentityResult>
    {
        public async Task<IdentityResult> Handle(RegisterWaiterCommand request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                UserName = request.Dto.Username,
                UserDetails = new UserDetails()
                {
                    FirstName = request.Dto.FirstName,
                    LastName = request.Dto.LastName,
                    Waiter = new Waiter()
                }
            };
            var userInsertion = await request.UserManager.CreateAsync(user, request.Dto.Password);

            if (userInsertion.Succeeded)
            {
                var WaiterInsertion = await request.UserManager.AddToRoleAsync(user, "waiter");

                if (WaiterInsertion.Succeeded)
                {
                    return IdentityResult.Success;
                }

            }
            return IdentityResult.Failed();
        }
    }
}