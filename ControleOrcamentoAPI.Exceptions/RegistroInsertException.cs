using System;

namespace ControleOrcamentoAPI.Exceptions
{
    public class RegistroInsertException : Exception
    {
        public RegistroInsertException() : this("Registro não incluído.")
        {

        }

        public RegistroInsertException(string mensagem) : base(mensagem)
        {

        }

        public RegistroInsertException(string mensagem, Exception innerException) : base(mensagem, innerException)
        {

        }
    }
}
