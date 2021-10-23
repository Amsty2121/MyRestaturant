using Microsoft.AspNetCore.Builder;
using WebApi.Infrastructure.Middlewares;

namespace WebApi.Infrastructure.Extensions
{
	public static class MiddlewareExtensions
	{
		public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
		{
			return app.UseMiddleware<ErrorHandlingMiddleware>();
		}
	}
}
