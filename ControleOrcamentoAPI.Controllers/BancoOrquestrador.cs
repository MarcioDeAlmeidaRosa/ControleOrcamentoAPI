using ControleOrcamentoAPI.DAO;
using System.Collections.Generic;
using ControleOrcamentoAPI.Models;

namespace ControleOrcamentoAPI.Orquestrador
{
    /// <summary>
    /// Responsável por Orquestrar Banco
    /// </summary>
    public class BancoOrquestrador : Orquestrador, IOrquestrador<Banco>
    {
        /// <summary>
        /// Resposnável por atualizar o registro na entidade do tipo Banco
        /// </summary>
        /// <param name="id">ID do registro da entidade informada no tipo Banco</param>
        /// <param name="entidade"> Entidade do tipo Banco contendo as informações que serão atualizadas no banco de dados</param>
        /// <param name="token"> Usuário logado na aplicação</param>
        /// <returns>Entidade atualizada no banco de dados</returns>
        public Banco Atualizar(long id, Banco entidade, UsuarioAutenticado token)
        {
            using (var dao = new BancoDAO(token))
            {
                return dao.Atualizar(id, entidade);
            }
        }

        /// <summary>
        /// Responsável por recuperar a entidade definida no tipo Banco pelo ID
        /// </summary>
        /// <param name="id">ID do registro da entidade informada no tipo Banco</param>
        /// <param name="token"> Usuário logado na aplicação</param>
        /// <returns>Objeto do tipo Banco encontrado pelo id informado</returns>
        public Banco BuscarPorID(long id, UsuarioAutenticado token)
        {
            using (var dao = new BancoDAO(token))
            {
                return dao.BuscarPorID(id);
            }
        }

        /// <summary>
        /// Resposnável por incluir novo registro na entidade na coleção do tipo Banco
        /// </summary>
        /// <param name="entidade"> Entidade do tipo Banco contendo as informações que serão inseridas no banco de dados</param>
        /// <param name="token"> Usuário logado na aplicação</param>
        /// <returns>Entidade do tipo Banco incluída no banco de dados</returns>
        public Banco Criar(Banco entidade, UsuarioAutenticado token)
        {
            using (var dao = new BancoDAO(token))
            {
                return dao.Criar(entidade);
            }
        }

        /// <summary>
        /// Resposnável por excluir logicamente o registro informado
        /// </summary>
        /// <param name="id">ID do registro da entidade informada no tipo Banco</param>
        /// <param name="token"> Usuário logado na aplicação</param>
        public void Deletar(long id, UsuarioAutenticado token)
        {
            using (var dao = new BancoDAO(token))
            {
                dao.Deletar(id);
            }
        }

        /// <summary>
        /// Responsável por recuperar uma lista da entidade definida no tipo Banco pelo campos da própria entidade
        /// </summary>
        /// <param name="entidade"> Entidade do tipo Banco contendo as informações que serão utilizadas para filtrar os dados</param>
        /// <param name="token"> Usuário logado na aplicação</param>
        /// <returns>Objetos encontrado pelo filtro informado do tipo Banco</returns>
        public IList<Banco> ListarPorEntidade(Banco entidade, UsuarioAutenticado token)
        {
            using (var dao = new BancoDAO(token))
            {
                return dao.ListarPorEntidade(entidade);
            }
        }
    }
}
