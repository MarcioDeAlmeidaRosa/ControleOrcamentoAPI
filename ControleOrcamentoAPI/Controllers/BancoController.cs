using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ControleOrcamentoAPI.Models;
using ControleOrcamentoAPI.Orquestrador;

namespace ControleOrcamentoAPI.Controllers
{
    [RoutePrefix("api/banco")]
    public class BancoController : BaseController
    {
        private static BancoOrquestrador _orquestrador;

        static BancoController()
        {
            _orquestrador = new BancoOrquestrador();
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
        public HttpResponseMessage Get([FromUri]Banco banco)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _orquestrador.ListarPorEntidade(banco, User));
            }
            catch (Exception ex)
            {
                return InternalErro(ex);
            }
        }

        [Authorize(Roles = "ADMIN")]
        public HttpResponseMessage Post()
        {
            return null;
        }
    }
}
