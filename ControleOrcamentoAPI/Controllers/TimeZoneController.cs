using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ControleOrcamentoAPI.Utils;

namespace ControleOrcamentoAPI.Controllers
{
    /// <summary>
    /// Controle responsável por auxiliar no controle de time zone da aplicação
    /// </summary>
    [RoutePrefix("api/timezone")]
    public class TimeZoneController : BaseController
    {
        /// <summary>
        /// R
        /// </summary>
        /// <returns>Retorna domínio de time zone possível para seleção ao cadastrar um usuário</returns>
        [AllowAnonymous]
        public HttpResponseMessage Get()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, HelpTimeZone.ListaTimeZone());
            }
            catch (Exception ex)
            {
                return InternalErro(ex);
            }
        }
    }
}
