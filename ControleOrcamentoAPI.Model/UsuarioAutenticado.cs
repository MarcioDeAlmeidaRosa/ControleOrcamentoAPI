using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleOrcamentoAPI.Models
{
    /// <summary>
    /// Classe responsável por definir os atributos de um usuário logado na aplicação
    /// </summary>
    [Table("USUARIO")]
    public class UsuarioAutenticado : Entity
    {
        /// <summary>
        /// Propriedade <see cref="Nome"/> responsável por armazenar o nome do usuário logado na aplicação
        /// </summary>
        [Column("NOME", TypeName = "ntext")]
        [StringLength(150)]
        public string Nome { get; set; }

        /// <summary>
        /// Propriedade <see cref="Email"/> responsável por armazenar o e-mail do usuário logado na aplicação
        /// </summary>
        [Column("EMAIL", TypeName = "ntext")]
        [StringLength(150)]
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Propriedade <see cref="Claim"/> responsável por armazenar a regra do usuário logado na aplicação
        /// </summary>
        [Column("CLAIM", TypeName = "ntext")]
        [StringLength(150)]
        [Required]
        public string Claim { get; set; }
    }
}
