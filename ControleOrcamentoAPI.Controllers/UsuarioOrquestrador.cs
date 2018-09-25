using ControleOrcamentoAPI.DAO;
using System.Collections.Generic;
using ControleOrcamentoAPI.Models;

namespace ControleOrcamentoAPI.Orquestrador
{
    /// <summary>
    /// Responsável por Orquestrar Usuário
    /// </summary>
    public class UsuarioOrquestrador : Orquestrador, IOrquestrador<Usuario>
    {
        /// <summary>
        /// Resposnável por atualizar o registro na entidade do tipo Usuário
        /// </summary>
        /// <param name="id">ID do registro da entidade informada no tipo Usuário</param>
        /// <param name="entidade"> Entidade do tipo Usuário contendo as informações que serão atualizadas no banco de dados</param>
        /// <param name="token"> Usuário logado na aplicação</param>
        /// <returns>Entidade atualizada no banco de dados</returns>
        public Usuario Atualizar(long id, Usuario entidade, UsuarioAutenticado token)
        {
            using (var dao = new UsuarioDAO(token))
            {
                return dao.Atualizar(id, entidade);
            }
        }

        /// <summary>
        /// Responsável por recuperar a entidade definida no tipo Usuário pelo ID
        /// </summary>
        /// <param name="id">ID do registro da entidade informada no tipo Usuário</param>
        /// <param name="token"> Usuário logado na aplicação</param>
        /// <returns>Objeto do tipo Usuário encontrado pelo id informado</returns>
        public Usuario BuscarPorID(long id, UsuarioAutenticado token)
        {
            using (var dao = new UsuarioDAO(token))
            {
                return dao.BuscarPorID(id);
            }
        }

        /// <summary>
        /// Resposnável por incluir novo registro na entidade na coleção do tipo Usuário
        /// </summary>
        /// <param name="entidade"> Entidade do tipo Usuário contendo as informações que serão inseridas no banco de dados</param>
        /// <param name="token"> Usuário logado na aplicação</param>
        /// <returns>Entidade do tipo Usuário incluída no banco de dados</returns>
        public Usuario Criar(Usuario entidade, UsuarioAutenticado token)
        {
            using (var dao = new UsuarioDAO(token))
            {
                return dao.Criar(entidade);
            }
        }

        /// <summary>
        /// Resposnável por excluir logicamente o registro informado
        /// </summary>
        /// <param name="id">ID do registro da entidade informada no tipo Usuário</param>
        /// <param name="token"> Usuário logado na aplicação</param>
        public void Deletar(long id, UsuarioAutenticado token)
        {
            using (var dao = new UsuarioDAO(token))
            {
                dao.Deletar(id);
            }
        }

        /// <summary>
        /// Responsável por recuperar uma lista da entidade definida no tipo Usuário pelo campos da própria entidade
        /// </summary>
        /// <param name="entidade"> Entidade do tipo Usuário contendo as informações que serão utilizadas para filtrar os dados</param>
        /// <param name="token"> Usuário logado na aplicação</param>
        /// <returns>Objetos encontrado pelo filtro informado do tipo Usuário</returns>
        public IList<Usuario> ListarPorEntidade(Usuario entidade, UsuarioAutenticado token)
        {
            using (var dao = new UsuarioDAO(token))
            {
                return dao.ListarPorEntidade(entidade);
            }
        }
    }
}
