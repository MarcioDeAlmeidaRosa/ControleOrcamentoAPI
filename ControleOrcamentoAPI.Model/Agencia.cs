namespace ControleOrcamentoAPI.Models
{
    /// <summary>
    /// Classe responsável por definir os atributos de uma agência na aplicação
    /// </summary>
    public class Agencia: Entity
    {
        /// <summary>
        /// Propriedade <see cref="Banco"/> responsável por armazenar o banco da agência na aplicação
        /// </summary>
        public Banco Banco { get; set; }

        /// <summary>
        /// Propriedade <see cref="Numero"/> responsável por armazenar o número da agência na aplicação
        /// </summary>
        public string Numero { get; set; }

        /// <summary>
        /// Propriedade <see cref="DV"/> responsável por armazenar o DV da agência na aplicação
        /// </summary>
        public string DV { get; set; }

        /// <summary>
        /// Propriedade <see cref="Descricao"/> responsável por armazenar a descrição da agência na aplicação
        /// </summary>
        public string Descricao { get; set; }
    }
}
