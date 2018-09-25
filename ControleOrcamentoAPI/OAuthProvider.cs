using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Owin.Security.OAuth;
using ControleOrcamentoAPI.Exceptions;
using ControleOrcamentoAPI.Orquestrador;

namespace ControleOrcamentoAPI
{
    /// <summary>
    /// Privider para configuração do Oauth
    /// </summary>
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// Responsável por efetuar login do usuário na aplicação
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                var authOrquestrador = new AuthOrquestrador();
                try
                {
                    var usuario = authOrquestrador.Login(context.UserName, context.Password);
                    if ((usuario != null) && (usuario.ID > 0))
                    {
                        IList<Claim> claims = new List<Claim>() {
                        new Claim(ClaimTypes.Name,!string.IsNullOrWhiteSpace(usuario.Nome)?usuario.Nome:usuario.Email ),
                        new Claim("UserID",usuario.ID.ToString() ),
                        new Claim(ClaimTypes.Role,usuario.Claim ),
                        new Claim("usuario", Newtonsoft.Json.JsonConvert.SerializeObject(usuario) ),
                        };
                        ClaimsIdentity oAuthIdentity = new ClaimsIdentity(claims, Startup.OAuthOptions.AuthenticationType);
                        context.Validated(new Microsoft.Owin.Security.AuthenticationTicket(oAuthIdentity, new Microsoft.Owin.Security.AuthenticationProperties() { }));
                        return;
                    }
                    context.SetError("3", "Usuário não autenticado");
                }
                catch (RegistroNaoEncontradoException ex)
                {
                    //Failure = 3
                    context.SetError("3", ex.Message);
                }
                catch (UsuarioBloqueadoException ex)
                {
                    // LockedOut = 1,
                    context.SetError("1", ex.Message);
                }
                catch (UsuarioNaoVerificadoException ex)
                {
                    //RequiresVerification = 2,
                    context.SetError("2", ex.Message);
                }
                catch (Exception ex)
                {
                    context.SetError("3", "Usuário não autenticado");
                }
            });
        }

        /// <summary>
        /// Valida credencias do usuário para acessar os métodos da API
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
                context.Validated();
            return Task.FromResult<object>(null);
        }
    }
}