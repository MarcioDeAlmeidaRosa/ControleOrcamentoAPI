using System.Collections.Generic;

namespace ControleOrcamentoAPI.Models.Comparadores
{
    /// <summary>
    /// Responsável por definir o comparador da Agencia considerando o Banco
    /// </summary>
    public class ComparadorAgenciaPorBanco : IComparer<Agencia>
    {
        /// <summary>
        /// Ordena lista de agência considerando o nome do banco como critério
        /// </summary>
        /// <param name="x"> Primeiro objeto agência para comparação</param>
        /// <param name="y"> Segundo objeto agência para comparação</param>
        /// <returns>Se primeiro objeto é menor que o segundo, será retornado -1, caso igual, será retornado 0, caso maior, será retornado 1</returns>
        public int Compare(Agencia x, Agencia y)
        {
            if (x == y) return 0;
            if (x == null) return 1;
            if (y == null) return -1;
            if (x.Banco == null) return 1;
            if (y.Banco == null) return -1;
            return x.Banco.Nome.CompareTo(y.Banco.Nome);
        }
    }
}
