using System;
using System.Net;
using System.Linq;
using Microsoft.Owin;
using System.Net.Http;
using System.Web.Http;
using ControleOrcamentoAPI.Models;

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

        protected HttpResponseMessage InternalErro(Exception ex)
        {
            //Appendix A. HTTP Response Status Codes
            //https://developer.yahoo.com/social/rest_api_guide/http-response-codes.html
            switch (ex.GetType().ToString())
            {
                case "ControleOrcamentoAPI.Exceptions.RegistroNaoEncontradoException":
                    return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError(ex.Message));
                case "ControleOrcamentoAPI.Exceptions.RegistroDuplicadoException":
                    return Request.CreateResponse(HttpStatusCode.Ambiguous, new HttpError(ex.Message));
                case "ControleOrcamentoAPI.Exceptions.RegistroUpdateException":
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(ex.Message));
                case "ControleOrcamentoAPI.Exceptions.RegistroInsertException":
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(ex.Message));
                case "ControleOrcamentoAPI.Exceptions.RegistroDeleteException":
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(ex.Message));
                case "System.ArgumentException":
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(ex.Message));
                default:
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("Erro interno do servidor"));
            }
        }
    }
}
