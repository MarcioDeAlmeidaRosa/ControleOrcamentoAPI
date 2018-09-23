using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ControleOrcamentoAPI.Utils;

namespace ControleOrcamentoAPI.Controllers
{
    [RoutePrefix("api/timezone")]
    public class TimeZoneController : BaseController
    {
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
