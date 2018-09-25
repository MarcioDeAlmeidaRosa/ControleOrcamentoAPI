using Owin;
using System;
using System.Configuration;
using Microsoft.Owin.Security.OAuth;
using ControleOrcamentoAPI.Models.Mapper;

namespace ControleOrcamentoAPI
{
    /// <summary>
    /// Classe parcial de inicializaão da aplicãção
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Propriedade de configuração da autenticação oauth
        /// </summary>
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        /// <summary>
        /// Configura a aplicação para utilizar Oauth
        /// </summary>
        /// <param name="app">Instância da aplicação</param>
        public void Configuration(IAppBuilder app)
        {
            app.UseOAuthBearerTokens(OAuthOptions);
        }

        /// <summary>
        /// Inicialização da aplicação
        /// </summary>
        static Startup()
        {
            AutoMapperConfig.RegisterMappings();
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