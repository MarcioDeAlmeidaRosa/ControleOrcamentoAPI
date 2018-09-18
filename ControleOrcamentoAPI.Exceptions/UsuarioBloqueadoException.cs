using System;

namespace ControleOrcamentoAPI.Exceptions
{
    public class UsuarioBloqueadoException : Exception
    {
        public UsuarioBloqueadoException() : this("Usuário bloqueado")
        {

        }

        public UsuarioBloqueadoException(string mensagem) : base(mensagem)
        {

        }

        public UsuarioBloqueadoException(string mensagem, Exception innerException) : base(mensagem, innerException)
        {

        }
    }
}
