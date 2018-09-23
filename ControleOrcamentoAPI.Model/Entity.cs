using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleOrcamentoAPI.Models
{
    /// <summary>
    /// Classe abstrata para definifr atributos obrigatórios para uma entidade
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Propriedade <see cref="ID"/> que definea a chave primária do registro na aplicação
        /// </summary>
        [Key]
        [Column("ID", TypeName = "bigint")]
        public long ID { get; set; }

        /// <summary>
        /// Propriedade <see cref="UsuarioInclusao"/> que define o usuário que incluiu o registro na aplicação
        /// </summary>
        [Column("USUARIO_INCLUSAO", TypeName = "bigint")]
        public long? UsuarioInclusao { get; set; }

        /// <summary>
        /// Propriedade <see cref="ID"/> que define a data de inclusão do registro na aplicação
        /// </summary>
        [Column("DATA_INCLUSAO", TypeName = "datetime")]
        [Required]
        public DateTime? DataInclusao { get; set; }

        /// <summary>
        /// Propriedade <see cref="UsuarioAlteracao"/> que define o usuário que atualizou o registro na aplicação
        /// </summary>
        [Column("USUARIO_ALTERACAO", TypeName = "bigint")]
        public long? UsuarioAlteracao { get; set; }

        /// <summary>
        /// Propriedade <see cref="DataAlteracao"/> que define a data de alteração do registro na aplicação
        /// </summary>
        [Column("DATA_ALTERACAO", TypeName = "datetime")]
        public DateTime? DataAlteracao { get; set; }

        /// <summary>
        /// Propriedade <see cref="UsuarioCancelamento"/> que define o usuário que cancelou o registro na aplicação
        /// </summary>
        [Column("USUARIO_CANCELAMENTO", TypeName = "bigint")]
        public long? UsuarioCancelamento { get; set; }

        /// <summary>
        /// Propriedade <see cref="DataCancelamento"/> que define a data de cancelamento do registro na aplicação
        /// </summary>
        [Column("DATA_CANCELAMENTO", TypeName = "datetime")]
        public DateTime? DataCancelamento { get; set; }
    }
}
