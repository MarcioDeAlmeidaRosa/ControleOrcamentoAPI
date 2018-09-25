using ControleOrcamentoAPI.DAO;
using ControleOrcamentoAPI.Models;

namespace ControleOrcamentoAPI.Orquestrador
{
    /// <summary>
    /// Responsável por Orquestrar Autenticação
    /// </summary>
    public class AuthOrquestrador : Orquestrador
    {
        /// <summary>
        /// Responsável por efetuar login na aplicação
        /// </summary>
        /// <param name="usuario">Login do usuário registrado na aplicação</param>
        /// <param name="senha">Senha do usuário registrado na aplicação</param>
        /// <returns></returns>
        public UsuarioAutenticado Login(string usuario, string senha)
        {
            using (var dao = new AuthDAO())
            {
                return dao.Login(usuario, senha);
            }
        }

        /// <summary>
        /// Responsável por inserir usuário na aplicação
        /// </summary>
        /// <param name="entidade">Entidade de usuário para registrar na aplicação</param>
        public void Registrar(Usuario entidade)
        {
            using (var dao = new AuthDAO())
            {
                dao.Registrar(entidade);
            }
        }
    }
}
