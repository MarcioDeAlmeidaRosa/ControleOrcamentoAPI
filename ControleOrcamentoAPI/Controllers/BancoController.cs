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

        public HttpResponseMessage Get(long id)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _orquestrador.BuscarPorID(id));
            }
            catch (Exception ex)
            {
                return InternalErro(ex);
            }
        }

        public HttpResponseMessage Get([FromUri]Banco banco)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _orquestrador.ListarPorEntidade(banco));
            }
            catch (Exception ex)
            {
                return InternalErro(ex);
            }
        }
    }
}
