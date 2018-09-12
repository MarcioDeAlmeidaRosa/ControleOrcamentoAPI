using System;

namespace ControleOrcamentoAPI.Exceptions
{
    public class VerificadoException : Exception
    {
        public VerificadoException(string mensagem) : base(mensagem)
        {

        }
    }
}
