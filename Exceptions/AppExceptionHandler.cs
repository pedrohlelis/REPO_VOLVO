using Microsoft.AspNetCore.Diagnostics;

namespace TRABALHO_VOLVO
{
    public class AppExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var response = new ErrorResponse()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ExceptionMessage = exception.Message,
                Title = "Erro !"
            };
            
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            
            return true;
        }
    }
}