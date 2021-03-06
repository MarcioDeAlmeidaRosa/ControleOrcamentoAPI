﻿using System;

namespace ControleOrcamentoAPI.Exceptions
{
    public class RegistroUpdateException : Exception
    {
        public RegistroUpdateException() : this("Registro não atualizado.")
        {

        }

        public RegistroUpdateException(string mensagem) : base(mensagem)
        {

        }

        public RegistroUpdateException(string mensagem, Exception innerException) : base(mensagem, innerException)
        {

        }
    }
}
