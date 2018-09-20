using System;

namespace ControleOrcamentoAPI.Exceptions
{
    public class RegistroDeleteException : Exception
    {
        public RegistroDeleteException() : this("Registro não deletado.")
        {

        }

        public RegistroDeleteException(string mensagem) : base(mensagem)
        {

        }

        public RegistroDeleteException(string mensagem, Exception innerException) : base(mensagem, innerException)
        {

        }
    }
}
