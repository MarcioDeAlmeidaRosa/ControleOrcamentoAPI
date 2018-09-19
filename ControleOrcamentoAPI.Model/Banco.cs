using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleOrcamentoAPI.Models
{
    /// <summary>
    /// Classe responsável por definir os atributos de um banco na aplicação
    /// </summary>
    [Table("BANCO")]
    public class Banco : Entity
    {
        /// <summary>
        /// Propriedade <see cref="Codigo"/> responsável por armazenar o código do banco na aplicação
        /// </summary>
        [Column("CODIGO", TypeName = "ntext")]
        [StringLength(10)]
        [Required]
        [ConcurrencyCheck]
        public string Codigo { get; set; }

        /// <summary>
        /// Propriedade <see cref="Nome"/> responsável por armazenar o nome do banco na aplicação
        /// </summary>
        [Column("NOME", TypeName = "ntext")]
        [StringLength(100)]
        [Required]
        public string Nome { get; set; }
    }
}
