using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleOrcamentoAPI.Models
{
    /// <summary>
    /// Responsável por definir os atributos de um banco na aplicação
    /// </summary>
    [Table("BANCO")]
    public partial class Banco : Entity, IComparable
    {
        /// <summary>
        /// Propriedade <see cref="Codigo"/> responsável por armazenar o código do banco na aplicação
        /// </summary>
        [Column("CODIGO", TypeName = "varchar")]
        [StringLength(10, ErrorMessage = "{0} pode conter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        [ConcurrencyCheck]
        public string Codigo { get; set; }

        /// <summary>
        /// Propriedade <see cref="Nome"/> responsável por armazenar o nome do banco na aplicação
        /// </summary>
        [Column("NOME", TypeName = "varchar")]
        [StringLength(100, ErrorMessage = "{0} pode conter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Nome { get; set; }

        /// <summary>
        /// Definindo ordenação principal da classe
        /// </summary>
        /// <param name="obj">Objeto à ser comparado ordenação</param>
        /// <returns>Valor negativo quando a instãncia é menor, 0 quando for igual e 1 quando a instância for maior que o objeto comparado</returns>
        public int CompareTo(object obj)
        {
            if (!(obj is Banco outroObj))
                return -1;
            return Nome.CompareTo(outroObj.Nome);
        }
    }
}
