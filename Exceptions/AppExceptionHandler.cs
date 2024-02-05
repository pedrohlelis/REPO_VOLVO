using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

namespace TRABALHO_VOLVO
{
    public class AppExceptionHandler(ILogger<AppExceptionHandler> logger) : IExceptionHandler
    {
        private readonly ILogger<AppExceptionHandler> logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

            var (StatusCode, Title) = MapException(exception);

            if(StatusCode == default)
            {
                return false;
            }

            // SE QUISER REGISTRAR AS EXCEPTIONS PREVISTAS E MANUSEADAS!!! 

            // using (StreamWriter writer = new StreamWriter(@"Exceptions\HandledExceptionsLog.txt", true))
            // {
            //     await writer.WriteLineAsync(JsonConvert.SerializeObject(new
            //     {
            //         DataHora = DateTime.Now.ToString("dd/MM/yy HH:mm:ss"),
            //         statusCode = StatusCode,
            //         machineName = Environment.MachineName,
            //         TraceId = traceId,
            //         Error = exception
            //     }));
            //     writer.Flush();
            // }

            //se quiser logar no terminal a exception (isso irá crashar a api com a exception!!!)
            // logger.LogError(
            //     exception,
            //     "Nao foi possivel processar request na maquina {MachineName}. Traceid: {traceID}",
            //     Environment.MachineName,
            //     traceId
            // );

            await Results.Problem(
                title: Title,
                statusCode: StatusCode
                // extensions: new Dictionary<string, object?>
                // {
                //     {"traceId", traceId}
                // }
            ).ExecuteAsync(httpContext);

            return true; //significa que o pipeline de manipulacao de excessoes encerra aqui
            //return false -> ira chamar o proximo middleware para fazer manipulacao de excessoes
        }

        private static (int StatusCode, string Title) MapException(Exception exception)
        {
            return exception switch //inserir aqui todas as excessoes e suas mensagens
            {
                // DbUpdateException => (StatusCodes.Status500InternalServerError, exception.Message),
                FormatoInvalidoException => (StatusCodes.Status400BadRequest, exception.Message),
                ArgumentOutOfRangeException => (StatusCodes.Status400BadRequest, exception.Message),
                _ => default
            };
        }

    }

}