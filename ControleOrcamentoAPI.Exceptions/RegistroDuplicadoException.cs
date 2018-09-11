using System;

namespace ControleOrcamentoAPI.Exceptions
{
    public class RegistroDuplicadoException : Exception
    {
        public RegistroDuplicadoException(string mensagem) : base(mensagem)
        {

        }
    }
}
