namespace ControleOrcamentoAPI.Models
{
    /// <summary>
    /// Classe responsável por definir os atributos de uma conta corrente na aplicação
    /// </summary>
    public class ContaCorrente : Entity
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
    }
}
