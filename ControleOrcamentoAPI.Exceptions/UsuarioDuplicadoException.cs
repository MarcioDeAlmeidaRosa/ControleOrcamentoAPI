using System;

namespace ControleOrcamentoAPI.Exceptions
{
    public class UsuarioDuplicadoException : Exception
    {
        public UsuarioDuplicadoException() : this("Usuário já existe.")
        {

        }

        public UsuarioDuplicadoException(string mensagem) : base(mensagem)
        {

        }

        public UsuarioDuplicadoException(string mensagem, Exception innerException) : base(mensagem, innerException)
        {

        }
    }
}
