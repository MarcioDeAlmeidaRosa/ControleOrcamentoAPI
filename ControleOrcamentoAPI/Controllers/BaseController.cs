using System;
using System.Net;
using System.Linq;
using Microsoft.Owin;
using System.Net.Http;
using System.Web.Http;
using ControleOrcamentoAPI.Models;
using ControleOrcamentoAPI.Exceptions;

namespace ControleOrcamentoAPI.Controllers
{
    public abstract class BaseController : ApiController
    {
        protected UsuarioAutenticado User
        {
            get
            {
                IOwinContext ctx = Request.GetOwinContext();
                var user = ctx.Authentication.User.Claims.FirstOrDefault(c => c.Type == "usuario").Value;
                return Newtonsoft.Json.Linq.JObject.Parse(user).ToObject<UsuarioAutenticado>();
            }
        }

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
