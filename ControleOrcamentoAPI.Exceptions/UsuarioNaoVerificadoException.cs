using System;

namespace ControleOrcamentoAPI.Exceptions
{
    public class UsuarioNaoVerificadoException : Exception
    {
        public UsuarioNaoVerificadoException() : this("Usuário não verificado.")
        {

        }

        public UsuarioNaoVerificadoException(string mensagem) : base(mensagem)
        {

        }

        public UsuarioNaoVerificadoException(string mensagem, Exception innerException) : base(mensagem, innerException)
        {

        }
    }
}
