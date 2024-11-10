using System.Text;

namespace Contatos.Shared.Extensions
{
    public static class ExceptionExtension
    {
        public static string ObterMensagens(this Exception exception)
        {
            StringBuilder mensagem = new StringBuilder();
            Exception currentException = exception;
            mensagem.Append(exception.Message);
            while (currentException.InnerException != null)
            {
                mensagem.Append($" InnerException: {currentException.InnerException.Message}");
                currentException = currentException.InnerException;
            }
            return mensagem.ToString();
        }
    }
}
