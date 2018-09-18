using System;

namespace ControleOrcamentoAPI.Exceptions
{
    public class UsuarioNaoEncontradoException : Exception
    {
        public UsuarioNaoEncontradoException() : this("Usuário não localizado.")
        {

        }

        public UsuarioNaoEncontradoException(string mensagem) : base(mensagem)
        {

        }

        public UsuarioNaoEncontradoException(string mensagem, Exception innerException) : base(mensagem, innerException)
        {

        }
    }
}
