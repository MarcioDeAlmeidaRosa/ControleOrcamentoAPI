using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ControleOrcamentoAPI.Models;
using ControleOrcamentoAPI.Orquestrador;

namespace ControleOrcamentoAPI.Controllers
{
    [RoutePrefix("api/agencia")]
    public class AgenciaController : BaseController
    {
        private static AgenciaOrquestrador _orquestrador;

        static AgenciaController()
        {
            _orquestrador = new AgenciaOrquestrador();
        }

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

        [Authorize(Roles = "ADMIN, USER")]
        public HttpResponseMessage Get([FromUri]Agencia entidade)
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

        [Authorize(Roles = "ADMIN, USER")]
        public HttpResponseMessage Post([FromBody]Agencia entidade)
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

        [Authorize(Roles = "ADMIN, USER")]
        public HttpResponseMessage Put(long id, [FromBody]Agencia entidade)
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

        [Authorize(Roles = "ADMIN, USER")]
        public HttpResponseMessage Delete([FromUri]long id)
        {
            try
            {
                _orquestrador.Deletar(id, User);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return InternalErro(ex);
            }
        }
    }
}
