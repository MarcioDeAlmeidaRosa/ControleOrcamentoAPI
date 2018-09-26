using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using ControleOrcamentoAPI.Models;
using ControleOrcamentoAPI.Orquestrador;

namespace ControleOrcamentoAPI.Controllers
{
    /// <summary>
    /// Controle responsável pelas funcionalidades de usuário na aplicação
    /// </summary>
    [RoutePrefix("api/usuario")]
    public class UsuarioController : BaseController
    {
        /// <summary>
        /// Propriedade responsável por armazenar a instãncia do orquestrador na aplicação
        /// </summary>
        private static readonly UsuarioOrquestrador _orquestrador;

        /// <summary>
        /// Construtor estático do controle para garantir uma única instância do orquestrador
        /// </summary>
        static UsuarioController()
        {
            _orquestrador = new UsuarioOrquestrador();
        }

        /// <summary>
        /// Action de pesquisa por ID do controle
        /// </summary>
        /// <param name="id">Id da usuário que deseja ser consultado</param>
        /// <returns>Entidade Usuário encontrado do banco de dados</returns>
        [Authorize(Roles = "ADMIN, USER")]
        public HttpResponseMessage Get(long id)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _orquestrador.BuscarPorID(id, User));
            }
            catch (Exception ex)
            {
                return InternalErro(ex);
            }
        }

        /// <summary>
        /// Action de listagem do controle
        /// </summary>
        /// <param name="entidade">Dados de consulta de um Usuário</param>
        /// <returns>Listage de Usuário cadastrado na aplicação</returns>
        [Authorize(Roles = "ADMIN, USER")]
        public HttpResponseMessage Get([FromUri]Usuario entidade)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _orquestrador.ListarPorEntidade(entidade, User));
            }
            catch (Exception ex)
            {
                return InternalErro(ex);
            }
        }

        /// <summary>
        /// Action de cadastro do controle
        /// </summary>
        /// <param name="entidade">Dados do Usuário para cadastrar na aplicação</param>
        /// <returns>Retorna o Usuário cadastrado na aplicação</returns>
        [Authorize(Roles = "ADMIN, USER")]
        public HttpResponseMessage Post([FromBody]Usuario entidade)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.Created, _orquestrador.Criar(entidade, User));
            }
            catch (Exception ex)
            {
                return InternalErro(ex);
            }
        }

        /// <summary>
        /// Action de atualização do controle
        /// </summary>
        /// <param name="id">ID do Usuário que será atualizado</param>
        /// <param name="entidade">Dados do Usuário para atualização</param>
        /// <returns>Usuário atualizado na aplicação</returns>
        [Authorize(Roles = "ADMIN, USER")]
        public HttpResponseMessage Put(long id, [FromBody]Usuario entidade)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, _orquestrador.Atualizar(id, entidade, User));
            }
            catch (Exception ex)
            {
                return InternalErro(ex);
            }
        }

        /// <summary>
        /// Action de deleção do controle
        /// </summary>
        /// <param name="id">ID do Usuário para deleção</param>
        /// <returns>Sucesso ou motivo do erro</returns>
        [Authorize(Roles = "ADMIN, USER")]
        public async Task<HttpResponseMessage> Delete([FromUri]long id)
        {
            try
            {
                await _orquestrador.Deletar(id, User);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return InternalErro(ex);
            }
        }
    }
}
