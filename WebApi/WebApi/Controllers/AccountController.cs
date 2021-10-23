using Common.Dto.User;
using Domain.Entities.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.Infrastructure.Configurations;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : BaseController
	{
		private readonly AuthOptions _authenticationOptions;
		private readonly IMediator _mediator;
		private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;

		public AccountController(IMediator mediator, IOptions<AuthOptions> authenticationOptions, SignInManager<User> signInManager, UserManager<User> userManager)
		{
			_authenticationOptions = authenticationOptions.Value;
			_mediator = mediator;
			_signInManager = signInManager;
			_userManager = userManager;
		}


		[AllowAnonymous]
		[HttpPost("login")]
		public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
		{
			var checkingPasswordResult = await _signInManager.PasswordSignInAsync(userForLoginDto.Username, userForLoginDto.Password, false, false);

			if (checkingPasswordResult.Succeeded)
			{
				var loggedInUser = await _userManager.FindByNameAsync(userForLoginDto.Username);

				var claims = new List<Claim>();

				var roles = await _userManager.GetRolesAsync(loggedInUser);
				foreach (var role in roles)
				{
					var roleClaim = new Claim(ClaimTypes.Role, role);
					claims.Add(roleClaim);

					var usernameClaim = new Claim(ClaimTypes.Name, userForLoginDto.Username);
					claims.Add(usernameClaim);
				}

				var signinCredentials = new SigningCredentials(_authenticationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
				var jwtSecurityToken = new JwtSecurityToken(
					issuer: _authenticationOptions.Issuer,
					audience: _authenticationOptions.Audience,
					claims: claims,
					expires: DateTime.Now.AddDays(30),
					signingCredentials: signinCredentials
				);

				var tokenHandler = new JwtSecurityTokenHandler();

				var encodedToken = tokenHandler.WriteToken(jwtSecurityToken);

				return Ok(new { AccessToken = encodedToken });
			}

			return Unauthorized();
		}
	}
}
