using Contatos.Shared.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Contatos.Api.Middlewares
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, $"Ocorreu um erro na aplicação: {exception.ObterMensagens()}");

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Error",
                Detail = exception.ObterMensagens()
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
