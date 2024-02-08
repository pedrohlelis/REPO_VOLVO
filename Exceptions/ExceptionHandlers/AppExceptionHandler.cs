using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

namespace TRABALHO_VOLVO
{
    public class AppExceptionHandler(ILogger<AppExceptionHandler> logger) : IExceptionHandler
    {
        private readonly ILogger<AppExceptionHandler> logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var (StatusCode, Title) = MapException(exception);

            if (StatusCode == default)
            {
                return false;
            }

            await httpContext.Response.WriteAsJsonAsync($"statusCode: {StatusCode}, Message: {Title}");

            return true;
        }

        private static (int StatusCode, string Title) MapException(Exception exception)
        {
            return exception switch
            {
                DuplicateUniqueValueException => (StatusCodes.Status404NotFound, exception.Message),
                DadosInsuficientesException => (StatusCodes.Status404NotFound, exception.Message),
                FKNotFoundException => (StatusCodes.Status400BadRequest, exception.Message),
                FormatoInvalidoException => (StatusCodes.Status400BadRequest, exception.Message),
                ArgumentOutOfRangeException => (StatusCodes.Status400BadRequest, exception.Message),
                _ => default
            };
        }

    }

}