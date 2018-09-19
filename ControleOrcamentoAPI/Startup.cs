using Owin;
using System;
using System.Configuration;
using Microsoft.Owin.Security.OAuth;

namespace ControleOrcamentoAPI
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            app.UseOAuthBearerTokens(OAuthOptions);
        }

        static Startup()
        {
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp = Convert.ToBoolean(ConfigurationManager.AppSettings["PERMITIR_CHAMADA_HTTP"]),
                TokenEndpointPath = new Microsoft.Owin.PathString("/api/auth/login"),
                Provider = new OAuthProvider()
            };
        }
    }
}