using System;

namespace ControleOrcamentoAPI.Exceptions
{
    public class RegistroDuplicadoException : Exception
    {
        public RegistroDuplicadoException() : this("Registro já existe.")
        {

        }

        public RegistroDuplicadoException(string mensagem) : base(mensagem)
        {

        }

        public RegistroDuplicadoException(string mensagem, Exception innerException) : base(mensagem, innerException)
        {

        }
    }
}
