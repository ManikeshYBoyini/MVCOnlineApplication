using System.Net;

namespace CodeChallengeApp.Middlewares
{
	public class CommonExceptionMiddleware : IMiddleware
	{
		private readonly ILogger<CommonExceptionMiddleware> _logger;
		public CommonExceptionMiddleware(ILogger<CommonExceptionMiddleware> logger)
		{
			_logger= logger;
		}		
		async Task IMiddleware.InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				await HandleRunTimeException(context, ex);
			}
		}

		private async Task HandleRunTimeException(HttpContext context, Exception ex)
		{
			string message;
			HttpStatusCode statusCode;
			if (ex is UnauthorizedAccessException)
			{
				message = "Unauthorized";
				statusCode = HttpStatusCode.Unauthorized;
			}
			else if (ex is ArgumentNullException)
			{
				message = "Bad Request";
				statusCode = HttpStatusCode.BadRequest;
			}
			else if (ex is WebException)
			{
				message = "Not Found";
				statusCode = HttpStatusCode.NotFound;
			}
			else
			{
				message = "Internal server error";
				statusCode = HttpStatusCode.InternalServerError;
			}
			_logger.LogError(ex, message + "  --  " + ex.Message.ToString() + "  --  " + statusCode.ToString());

			//Redirects to generic error page
			context.Response.Redirect("/Home/CustomError");
		}
	}

	/// <summary>
	/// Extension method for initializing CommonExceptionMiddleware
	/// </summary>
	public static class GlobalExceptionMiddleware
	{
		public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder applicationBuilder)
		{
			return applicationBuilder.UseMiddleware<CommonExceptionMiddleware>();
		}
	}
}
