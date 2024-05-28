using Contatos.Api.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Contatos.Test.Api.Middlewares
{
    public class ExceptionHandlerTest
    {
        private readonly Mock<ILogger<ExceptionHandler>> _loggerMock;
        private readonly ExceptionHandler _exceptionHandler;

        public ExceptionHandlerTest()
        {
            _loggerMock = new Mock<ILogger<ExceptionHandler>>();
            _exceptionHandler = new ExceptionHandler(_loggerMock.Object);
        }

        [Fact]
        public async Task TratarAsync_DeveRetornarTrueQuandoExcecaoOcorre()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Response.Body = new MemoryStream();
            var exception = new Exception("Erro de teste");

            var resultado = await _exceptionHandler.TryHandleAsync(httpContext, exception, new CancellationToken());

            Assert.True(resultado);

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(httpContext.Response.Body).ReadToEndAsync();

            Assert.Contains("Erro de teste", responseBody);
        }
    }
}
