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
    /// Controle responsável pelas funcionalidades da banco na aplicação
    /// </summary>
    [RoutePrefix("api/banco")]
    public class BancoController : BaseController
    {
        /// <summary>
        /// Propriedade responsável por armazenar a instãncia do orquestrador na aplicação
        /// </summary>
        private static BancoOrquestrador _orquestrador;

        /// <summary>
        /// Construtor estático do controle para garantir uma única instância do orquestrador
        /// </summary>
        static BancoController()
        {
            _orquestrador = new BancoOrquestrador();
        }

        /// <summary>
        /// Action de pesquisa por ID do controle
        /// </summary>
        /// <param name="id">Id do Banco que deseja ser consultad</param>
        /// <returns>Entidade Banco encontrado do banco de dados</returns>
        [Authorize(Roles = "ADMIN, USER")]
        public async Task<HttpResponseMessage> Get(long id)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, await _orquestrador.BuscarPorID(id, User));
            }
            catch (Exception ex)
            {
                return InternalErro(ex);
            }
        }

        /// <summary>
        /// Action de listagem do controle
        /// </summary>
        /// <param name="entidade">Dados de consulta de um Banco</param>
        /// <returns>Listage de Banco cadastrado na aplicação</returns>
        [Authorize(Roles = "ADMIN, USER")]
        public async Task<HttpResponseMessage> Get([FromUri]Banco entidade)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, await _orquestrador.ListarPorEntidade(entidade, User));
            }
            catch (Exception ex)
            {
                return InternalErro(ex);
            }
        }

        /// <summary>
        /// Action de cadastro do controle
        /// </summary>
        /// <param name="entidade">Dados do Banco para cadastrar na aplicação</param>
        /// <returns>Retorna o Banco cadastrado na aplicação</returns>
        [Authorize(Roles = "ADMIN")]
        public async Task<HttpResponseMessage> Post([FromBody]Banco entidade)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.Created, await _orquestrador.Criar(entidade, User));
            }
            catch (Exception ex)
            {
                return InternalErro(ex);
            }
        }

        /// <summary>
        /// Action de atualização do controle
        /// </summary>
        /// <param name="id">ID do Banco que será atualizado</param>
        /// <param name="entidade">Dados do Banco para atualização</param>
        /// <returns>Banco atualizado na aplicação</returns>
        [Authorize(Roles = "ADMIN")]
        public async Task<HttpResponseMessage> Put(long id, [FromBody]Banco entidade)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, await _orquestrador.Atualizar(id, entidade, User));
            }
            catch (Exception ex)
            {
                return InternalErro(ex);
            }
        }

        /// <summary>
        /// Action de deleção do controle
        /// </summary>
        /// <param name="id">ID do Banco para deleção</param>
        /// <returns>Sucesso ou motivo do erro</returns>
        [Authorize(Roles = "ADMIN")]
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
