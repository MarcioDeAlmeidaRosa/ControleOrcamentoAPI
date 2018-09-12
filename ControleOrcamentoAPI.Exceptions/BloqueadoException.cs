using System;

namespace ControleOrcamentoAPI.Exceptions
{
    public class BloqueadoException : Exception
    {
        public BloqueadoException(string mensagem) : base(mensagem)
        {

        }
    }
}
