using System;

namespace ControleOrcamentoAPI.Exceptions
{
    public class RegistroNaoEncontradoException : Exception
    {
        public RegistroNaoEncontradoException() : this("Registro não localizado.")
        {

        }

        public RegistroNaoEncontradoException(string mensagem) : base(mensagem)
        {

        }

        public RegistroNaoEncontradoException(string mensagem, Exception innerException) : base(mensagem, innerException)
        {

        }
    }
}
