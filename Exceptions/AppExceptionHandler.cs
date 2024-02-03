using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Diagnostics;
// using System.IO;
// using System.Reflection.PortableExecutable;
// using System.Security.Cryptography;

namespace TRABALHO_VOLVO
{
    public class AppExceptionHandler(ILogger<AppExceptionHandler> logger) : IExceptionHandler
    {
        private readonly ILogger<AppExceptionHandler> logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

            var (StatusCode, Title) = MapException(exception);
            using (StreamWriter writer = new StreamWriter(@"Exceptions\exceptionsLog.txt", true))
            {
                await writer.WriteLineAsync(JsonConvert.SerializeObject(new
                {
                    DataHora = DateTime.Now.ToString("dd/MM/yy HH:mm:ss"),
                    statusCode = StatusCode,
                    machineName = Environment.MachineName,
                    TraceId = traceId,
                    Error = exception
                }));
                writer.Flush();
            }
            logger.LogError(
                exception,
                "Nao foi possivel processar request na maquina {MachineName}. Traceid: {traceID}",
                Environment.MachineName,
                traceId
            );

            await Results.Problem(
                title: Title,
                statusCode: StatusCode,
                extensions: new Dictionary<string, object?>
                {
                    {"traceId", traceId}
                }
            ).ExecuteAsync(httpContext);

            return true; //significa que o pipeline de manipulacao de excessoes encerra aqui
            //return false -> ira chamar o proximo middleware para fazer manipulacao de excessoes
        }

        private static (int StatusCode, string Title) MapException(Exception exception)
        {
            return exception switch //inserir aqui todas as excessoes e suas mensagens
            {
                ArgumentOutOfRangeException => (StatusCodes.Status400BadRequest, "Informe os dados corretamente."),
                _ => (StatusCodes.Status500InternalServerError, "Encontramos um problema e estamos trabalhando para o resolver. Tente novamente mais Tarde.")
            };
        }

    }

}