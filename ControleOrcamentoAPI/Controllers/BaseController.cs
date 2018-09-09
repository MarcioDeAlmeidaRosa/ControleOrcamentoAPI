using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ControleOrcamentoAPI.Exceptions;

namespace ControleOrcamentoAPI.Controllers
{
    public abstract class BaseController : ApiController
    {
        protected HttpResponseMessage InternalErro(Exception exception)
        {
            HttpError erro = null;
            if (exception.GetType() == typeof(NotFoundException))
            {
                erro = new HttpError(exception.Message);
                return Request.CreateResponse(HttpStatusCode.NotFound, erro);
            }
            erro = new HttpError("Erro interno do servidor");
            return Request.CreateResponse(HttpStatusCode.InternalServerError, erro);
        }
    }
}
