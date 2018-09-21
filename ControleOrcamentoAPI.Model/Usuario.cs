using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleOrcamentoAPI.Models
{
    /// <summary>
    /// Responsável por definir os atributos de um usuário da aplicação
    /// </summary>
    [Table("USUARIO")]
    public partial class Usuario : Entity, IComparable
    {
        /// <summary>
        /// Propriedade <see cref="Nome"/> responsável por armazenar o primeiro nome do usuário na aplicação
        /// </summary>
        [Column("NOME", TypeName = "varchar")]
        [StringLength(50, ErrorMessage = "{0} pode conter no máximo {1} caracteres.")]
        public string Nome { get; set; }

        /// <summary>
        /// Propriedade <see cref="SobreNome"/> responsável por armazenar o restante do nome do usuário na aplicação
        /// </summary>
        [Column("SOBRENOME", TypeName = "varchar")]
        [StringLength(100, ErrorMessage = "{0} poode conter no máximo {1} caracteres.")]
        public string SobreNome { get; set; }

        /// <summary>
        /// Propriedade <see cref="Email"/> responsável por armazenar o e-mail do usuário na aplicação
        /// </summary>
        [Column("EMAIL", TypeName = "varchar")]
        [StringLength(254, ErrorMessage = "{0} poode conter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "E-mail inválido!")]
        public string Email { get; set; }

        /// <summary>
        /// Propriedade <see cref="Login"/> responsável por armazenar o login do usuário na aplicação
        /// </summary>
        [Column("LOGIN", TypeName = "varchar")]
        [StringLength(254, ErrorMessage = "{0} poode conter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "E-mail do login inválido!")]
        [ConcurrencyCheck]
        public string Login { get; set; }

        /// <summary>
        /// Propriedade <see cref="Senha"/> responsável por armazenar a senha do usuário na aplicação
        /// </summary>
        [Column("SENHA", TypeName = "varchar")]
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Senha { get; set; }

        /// <summary>
        /// Propriedade <see cref="Claim"/> responsável por armazenar o papel do usuário na aplicação (ADMIN/USER)
        /// </summary>
        [Column("CLAIM", TypeName = "varchar")]
        [StringLength(20, ErrorMessage = "{0} poode conter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Claim { get; set; }

        /// <summary>
        /// Propriedade que define a data de verificação da conta do usuário na aplicação
        /// </summary>
        [Column("DATA_VERIFICACAO", TypeName = "date")]
        public DateTime? DataVerificacao { get; set; }

        /// <summary>
        /// Propriedade <see cref="Verificado"/> responsável por armazenar o flag de usuário verificado na aplicação (ADMIN/USER)
        /// </summary>
        [Column("VERIFICADO", TypeName = "bit")]
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public bool Verificado { get; set; }

        /// <summary>
        /// Propriedade <see cref="Salt"/> responsável por armazenar o Salt da senha do usuário na aplicação
        /// </summary>
        [Column("SALT", TypeName = "varchar")]
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Salt { get; set; }//TODO: VER SE ACEITA  INTERNAL

        /// <summary>
        /// Propriedade <see cref="Bloqueado"/> responsável por armazenar o flag de usuário bloqueado na aplicação (ADMIN/USER)
        /// </summary>
        [Column("BLOQUEADO", TypeName = "bit")]
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public bool Bloqueado { get; set; }

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

        /// <summary>
        /// Definindo ordenação principal da classe
        /// </summary>
        /// <param name="obj">Objeto à ser comparado ordenação</param>
        /// <returns>Valor negativo quando a instãncia é menor, 0 quando for igual e 1 quando a instância for maior que o objeto comparado</returns>
        public int CompareTo(object obj)
        {
            if (!(obj is Usuario outroObj))
                return -1;
            return Nome.CompareTo(outroObj.Nome);
        }
    }
}
