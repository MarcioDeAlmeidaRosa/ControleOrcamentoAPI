using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ControleOrcamentoAPI.Models;
using ControleOrcamentoAPI.Orquestrador;

namespace ControleOrcamentoAPI.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : BaseController
    {
        private static AuthOrquestrador _orquestrador;

        static AuthController()
        {
            _orquestrador = new AuthOrquestrador();
        }

        [Route("Registrar")]
        [HttpPost]
        public HttpResponseMessage Registrar(Usuario usuario)
        {
            try
            {
                _orquestrador.Registrar(usuario);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return InternalErro(ex);
            }
        }

        [Route("ValidarToken")]
        [HttpPost]
        [Authorize(Roles = "ADMIN, USER")]
        public HttpResponseMessage ValidarToken(UsuarioAutenticado token)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _orquestrador.ValidaToken(token));
            }
            catch (Exception ex)
            {
                return InternalErro(ex);
            }
        }
    }
}
