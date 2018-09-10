using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Owin.Security.OAuth;
using ControleOrcamentoAPI.Orquestrador;

namespace ControleOrcamentoAPI
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                var authOrquestrador = new AuthOrquestrador();
                try
                {
                    var usuario = authOrquestrador.Login(context.UserName, context.Password);
                    if ((usuario != null) && (!string.IsNullOrWhiteSpace(usuario.Nome)))
                    {
                        IList<Claim> claims = new List<Claim>() {
                        new Claim(ClaimTypes.Name,usuario.Nome ),
                        new Claim("UserID",usuario.ID.ToString() ),
                        new Claim(ClaimTypes.Role,usuario.Role ),
                        new Claim("usuario", usuario.ToString() ),
                        };
                        ClaimsIdentity oAuthIdentity = new ClaimsIdentity(claims, Startup.OAuthOptions.AuthenticationType);
                        context.Validated(new Microsoft.Owin.Security.AuthenticationTicket(oAuthIdentity, new Microsoft.Owin.Security.AuthenticationProperties() { }));
                        return;
                    }
                    context.SetError("erro", "Usuário não autenticado");
                }
                catch (Exception)
                {
                    context.SetError("erro", "Usuário não autenticado");
                }
            });
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
                context.Validated();
            return Task.FromResult<object>(null);
        }
    }
}