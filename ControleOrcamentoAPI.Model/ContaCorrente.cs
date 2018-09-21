using System;

namespace ControleOrcamentoAPI.Models
{
    /// <summary>
    /// Responsável por definir os atributos de uma conta corrente na aplicação
    /// </summary>
    public partial class ContaCorrente : Entity, IComparable
    {
        /// <summary>
        /// Propriedade <see cref="Agencia"/> responsável por armazenar a agência da conta corrente na aplicação
        /// </summary>
        public Agencia Agencia { get; set; }

        /// <summary>
        /// Propriedade <see cref="Numero"/> responsável por armazenar o número da conta corrente na aplicação
        /// </summary>
        public string Numero { get; set; }

        /// <summary>
        /// Propriedade <see cref="DV"/> responsável por armazenar o número verificador da conta corrente na aplicação
        /// </summary>
        public string DV { get; set; }

        /// <summary>
        /// Definindo ordenação principal da classe
        /// </summary>
        /// <param name="obj">Objeto à ser comparado ordenação</param>
        /// <returns>Valor negativo quando a instãncia é menor, 0 quando for igual e 1 quando a instância for maior que o objeto comparado</returns>
        public int CompareTo(object obj)
        {
            if (!(obj is ContaCorrente outroObj))
                return -1;
            return Numero.CompareTo(outroObj.Numero);
        }
    }
}
