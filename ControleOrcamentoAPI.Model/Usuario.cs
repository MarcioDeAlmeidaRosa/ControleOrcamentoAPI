using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleOrcamentoAPI.Models
{
    /// <summary>
    /// Classe responsável por definir os atributos de um usuário da aplicação
    /// </summary>
    [Table("USUARIO")]
    public class Usuario : Entity
    {
        /// <summary>
        /// Propriedade <see cref="Nome"/> responsável por armazenar o primeiro nome do usuário na aplicação
        /// </summary>
        [Column("NOME", TypeName = "ntext")]
        [StringLength(150)]
        public string Nome { get; set; }

        /// <summary>
        /// Propriedade <see cref="SobreNome"/> responsável por armazenar o restante do nome do usuário na aplicação
        /// </summary>
        [Column("SOBRENOME", TypeName = "ntext")]
        [StringLength(150)]
        public string SobreNome { get; set; }

        /// <summary>
        /// Propriedade <see cref="Email"/> responsável por armazenar o e-mail do usuário na aplicação
        /// </summary>
        [Column("EMAIL", TypeName = "ntext")]
        [StringLength(254)]
        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "E-mail inválido!")]
        public string Email { get; set; }

        /// <summary>
        /// Propriedade <see cref="Login"/> responsável por armazenar o login do usuário na aplicação
        /// </summary>
        [Column("LOGIN", TypeName = "ntext")]
        [StringLength(254)]
        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "E-mail do login inválido!")]
        [ConcurrencyCheck]
        public string Login { get; set; }

        /// <summary>
        /// Propriedade <see cref="Senha"/> responsável por armazenar a senha do usuário na aplicação
        /// </summary>
        [Column("SENHA", TypeName = "ntext")]
        [Required]
        public string Senha { get; set; }

        /// <summary>
        /// Propriedade <see cref="Claim"/> responsável por armazenar o papel do usuário na aplicação (ADMIN/USER)
        /// </summary>
        [Column("CLAIM", TypeName = "ntext")]
        [StringLength(20)]
        [Required]
        public string Claim { get; set; }

        /// <summary>
        /// Propriedade <see cref="Verificado"/> responsável por armazenar o flag de usuário verificado na aplicação (ADMIN/USER)
        /// </summary>
        [Column("VERIFICADO", TypeName = "bit")]
        [Required]
        public bool Verificado { get; set; }

        /// <summary>
        /// Propriedade <see cref="Bloqueado"/> responsável por armazenar o flag de usuário bloqueado na aplicação (ADMIN/USER)
        /// </summary>
        [Column("BLOQUEADO", TypeName = "bit")]
        [Required]
        public bool Bloqueado { get; set; }

        /// <summary>
        /// Propriedade <see cref="Salt"/> responsável por armazenar o Salt da senha do usuário na aplicação
        /// </summary>
        [Column("SALT", TypeName = "ntext")]
        [Required]
        public string Salt { get; set; }//TODO: VER SE ACEITA  INTERNAL

        /// <summary>
        /// Propriedade que define a data de verificação da conta do usuário na aplicação
        /// </summary>
        [Column("DATA_VERIFICACAO", TypeName = "date")]
        public DateTime? DataVerificacao { get; set; }

        /// <summary>
        /// Propriedade que define a data de bloquei do usuário na aplicação
        /// </summary>
        [Column("DATA_BLOQUEIO", TypeName = "date")]
        public DateTime? DataBloqueio { get; set; }

        /// <summary>
        /// Propriedade <see cref="UsuarioBloqueio"/> que define o usuário que bloqueou o usuário na aplicação
        /// </summary>
        [Column("USUARIO_BLOQUEIO", TypeName = "bigint")]
        public long UsuarioBloqueio { get; set; }
    }
}
