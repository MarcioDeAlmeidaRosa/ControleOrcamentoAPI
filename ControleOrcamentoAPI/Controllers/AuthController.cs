using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ControleOrcamentoAPI.Models;
using ControleOrcamentoAPI.Orquestrador;

namespace ControleOrcamentoAPI.Controllers
{
    /// <summary>
    /// Controle responsável pela parte de segurança da aplicação
    /// </summary>
    [RoutePrefix("api/auth")]
    public class AuthController : BaseController
    {
        /// <summary>
        /// Propriedade responsável por armazenar a instãncia do orquestrador na aplicação
        /// </summary>
        private static AuthOrquestrador _orquestrador;

        /// <summary>
        /// Construtor estático do controle para garantir uma única instância do orquestrador
        /// </summary>
        static AuthController()
        {
            _orquestrador = new AuthOrquestrador();
        }

        /// <summary>
        /// Action que registra o usuário na aplicação
        /// </summary>
        /// <param name="usuario">Dados do usuário para registro inicial na aplicação</param>
        /// <returns>Status OK ou motivo do erro</returns>
        [Route("registrar")]
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
    }
}
