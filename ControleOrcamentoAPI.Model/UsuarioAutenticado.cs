namespace ControleOrcamentoAPI.Models
{
    /// <summary>
    /// Responsável por definir os atributos de um usuário logado na aplicação
    /// </summary>
    public partial class UsuarioAutenticado : Entity
    {
        /// <summary>
        /// Propriedade <see cref="Nome"/> responsável por armazenar o nome do usuário logado na aplicação
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Propriedade <see cref="Email"/> responsável por armazenar o e-mail do usuário logado na aplicação
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Propriedade <see cref="Claim"/> responsável por armazenar a regra do usuário logado na aplicação
        /// </summary>
        public string Claim { get; set; }
    }
}
