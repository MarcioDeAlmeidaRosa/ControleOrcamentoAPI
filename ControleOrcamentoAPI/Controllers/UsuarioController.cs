using System;
using System.Net.Http;
using System.Web.Http;

namespace ControleOrcamentoAPI.Controllers
{
    [RoutePrefix("api/usuario")]
    public class UsuarioController : BaseController
    {
        public HttpResponseMessage Get(long id)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                return InternalErro(ex);
            }
        }

    }
}
