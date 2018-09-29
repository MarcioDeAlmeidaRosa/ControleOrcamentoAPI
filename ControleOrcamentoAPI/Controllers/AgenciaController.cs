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
    /// Controle responsável pelas funcionalidades da agência na aplicação
    /// </summary>
    [RoutePrefix("api/agencia")]
    public class AgenciaController : BaseController
    {
        /// <summary>
        /// Propriedade responsável por armazenar a instãncia do orquestrador na aplicação
        /// </summary>
        private static AgenciaOrquestrador _orquestrador;

        /// <summary>
        /// Construtor estático do controle para garantir uma única instância do orquestrador
        /// </summary>
        static AgenciaController()
        {
            _orquestrador = new AgenciaOrquestrador();
        }

        /// <summary>
        /// Action de pesquisa por ID do controle
        /// </summary>
        /// <param name="id">Id da Agência que deseja ser consultada</param>
        /// <returns>Entidade Agência encontrada do banco de dados</returns>
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
        /// <param name="entidade">Dados de consulta de uma Agência</param>
        /// <returns>Listage de Agência cadastrada na aplicação</returns>
        [Authorize(Roles = "ADMIN, USER")]
        public async Task<HttpResponseMessage> Get([FromUri]Agencia entidade)
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
        /// <param name="entidade">Dados da Agência para cadastrar na aplicação</param>
        /// <returns>Agência cadastrada na aplicação</returns>
        [Authorize(Roles = "ADMIN, USER")]
        public async Task<HttpResponseMessage> Post([FromBody]Agencia entidade)
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
        /// <param name="id">ID da Agênca que será atualizada</param>
        /// <param name="entidade">Dados da Agência para atualização</param>
        /// <returns>Agência atualizada na aplicação</returns>
        [Authorize(Roles = "ADMIN, USER")]
        public async Task<HttpResponseMessage> Put(long id, [FromBody]Agencia entidade)
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
        /// <param name="id">ID da Agência para deleção</param>
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
