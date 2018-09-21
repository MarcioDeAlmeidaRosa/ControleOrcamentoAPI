using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleOrcamentoAPI.Models
{
    /// <summary>
    /// Responsável por definir os atributos de uma agência na aplicação
    /// </summary>
    [Table("AGENCIA")]
    public partial class Agencia : Entity, IComparable
    {
        /// <summary>
        /// Propriedade <see cref="BancoID"/> responsável por armazenar o ID banco da agência na aplicação
        /// </summary>
        [Column("BANCO_ID", TypeName = "bigint")]
        public long BancoID { get; set; }

        /// <summary>
        /// Propriedade <see cref="Banco"/> responsável por armazenar o objeto do banco da agência na aplicação
        /// </summary>
        [ForeignKey("BancoID")]
        public virtual Banco Banco { get; set; }

        /// <summary>
        /// Propriedade <see cref="Numero"/> responsável por armazenar o número da agência na aplicação
        /// </summary>
        [Column("NUMERO", TypeName = "varchar")]
        [StringLength(5, ErrorMessage = "{0} poode conter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "{0} é obrigatório.")]
        public string Numero { get; set; }

        /// <summary>
        /// Propriedade <see cref="DV"/> responsável por armazenar o DV da agência na aplicação
        /// </summary>
        [Column("DV", TypeName = "varchar")]
        [StringLength(1, ErrorMessage = "{0} poode conter no máximo {1} caracteres.")]
        public string DV { get; set; }

        /// <summary>
        /// Definindo ordenação principal da classe
        /// </summary>
        /// <param name="obj">Objeto à ser comparado ordenação</param>
        /// <returns>Valor negativo quando a instãncia é menor, 0 quando for igual e 1 quando a instância for maior que o objeto comparado</returns>
        public int CompareTo(object obj)
        {
            if (!(obj is Agencia outroObj))
                return -1;
            return Numero.CompareTo(outroObj.Numero);
        }
    }
}
