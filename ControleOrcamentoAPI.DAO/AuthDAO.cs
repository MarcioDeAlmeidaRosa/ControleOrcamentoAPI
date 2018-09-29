using System;
using System.Linq;
using System.Data;
using System.Text;
using System.Configuration;
using ControleOrcamentoAPI.Utils;
using ControleOrcamentoAPI.Models;
using System.Data.Entity.Validation;
using System.Web.Script.Serialization;
using ControleOrcamentoAPI.Extensoes;
using ControleOrcamentoAPI.Exceptions;
using ControleOrcamentoAPI.Criptografia;

namespace ControleOrcamentoAPI.DAO
{
    /// <summary>
    /// Responsável pela parte de autenticação de usuário na aplicação
    /// </summary>
    public class AuthDAO : DAO<Usuario>
    {
        /// <summary>
        /// propriedade responsável por armazenar a configuração do tamanho do Salt que a aplicação irá utilizar
        /// </summary>
        private static readonly int _LengthSalt = 0;

        /// <summary>
        /// Construtor static inicializando variáveis que não serão alteradas enquanto o serviço esta rodando
        /// </summary>
        static AuthDAO()
        {
            try
            {
                _LengthSalt = 32;
                int.TryParse(ConfigurationManager.AppSettings["LENGT_HSALT"].ToString(), out _LengthSalt);
            }
            catch (Exception)
            {
                _LengthSalt = 32;
            }
        }

        /// <summary>
        /// Responsável por efetuar o login do usuário na aplicação
        /// </summary>
        /// <param name="login"> Usuário de autenticação</param>
        /// <param name="senha"> Senha do usuário para autenticação</param>
        /// <returns>Informações básicas para autenticar o usuário</returns>
        public UsuarioAutenticado Login(string login, string senha)
        {
            var entidadeLocalizada = _dbContext.Usuarios.Where(data => data.Login.Equals(login, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            if (entidadeLocalizada == null)
                throw new RegistroNaoEncontradoException("Usuário não localizado.");

            JavaScriptSerializer js = new JavaScriptSerializer();
            byte[] SaltDeSerializado = js.Deserialize<byte[]>(entidadeLocalizada.Salt);
            Salt _Salt = new Salt(_LengthSalt);
            byte[] _senha = _Salt.GenerateDerivedKey(_LengthSalt, Encoding.UTF8.GetBytes(senha), SaltDeSerializado, 5000);
            if (entidadeLocalizada.Senha != _Salt.getPassword(_senha))
                throw new RegistroNaoEncontradoException("Usuário não localizado.");

            if ((!entidadeLocalizada.Verificado.HasValue) && (!entidadeLocalizada.Verificado.Value))
                throw new UsuarioNaoVerificadoException("Usuário não verificado.");

            if ((entidadeLocalizada.Bloqueado.HasValue) && (entidadeLocalizada.Bloqueado.Value))
                throw new UsuarioBloqueadoException("Usuário bloqueado.");

            if (entidadeLocalizada.DataCancelamento.HasValue)
                throw new RegistroNaoEncontradoException("Usuário cancelado.");

            return new UsuarioAutenticado()
            {
                ID = entidadeLocalizada.ID,
                Nome = entidadeLocalizada.Nome,
                Email = entidadeLocalizada.Email,
                Claim = entidadeLocalizada.Claim,
                TimeZone = entidadeLocalizada.TimeZone
            };
        }

        /// <summary>
        /// Responsável por efetuar o registro de novos usuários na aplicação
        /// </summary>
        /// <param name="entidade"> Entidade contendo informações do novo usuário à ser registrado</param>
        public void Registrar(Usuario entidade)
        {
            try
            {
                var entidadeLocalizada = _dbContext.Usuarios.Where(data => data.Login.Equals(entidade.Login, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                if (entidadeLocalizada != null)
                    throw new RegistroDuplicadoException("Usuário já existe na aplicação");

                Salt _Salt = new Salt(_LengthSalt);
                JavaScriptSerializer js = new JavaScriptSerializer();
                byte[] SaltDeSerializado = _Salt.GenerateSalt();
                string SerializeSalt = js.Serialize(SaltDeSerializado);
                byte[] result = _Salt.GenerateDerivedKey(_LengthSalt, Encoding.UTF8.GetBytes(entidade.Senha), SaltDeSerializado, 5000);
                entidade.Login = entidade.Email;
                entidade.Senha = _Salt.getPassword(result);
                entidade.Salt = SerializeSalt;
                entidade.Claim = "USER";
                if (string.IsNullOrWhiteSpace(entidade.TimeZone) && (HelpTimeZone.IsIDUTCValid(entidade.TimeZone)))
                    entidade.TimeZone = HelpTimeZone.RecuperarIDTimeZonePadrao();
                entidade.Bloqueado = true;
                entidade.DataInclusao = DateTime.UtcNow;
                entidade.DataBloqueio = DateTime.UtcNow;
                entidade.Verificado = false;
                _dbContext.Usuarios.Add(entidade);
                _dbContext.SaveChanges();
                entidade.UsuarioInclusao = entidade.ID;
                _dbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var st = new StringBuilder();
                ex.EntityValidationErrors.ToList().ForEach(errs => errs.ValidationErrors.ToList().ForEach(err => st.AppendLine(err.ErrorMessage)));
                //TODO AJUSTAR O EXCEPTION DO RETORNO
                throw new Exception(st.ToString(), ex);
            }
        }
    }
}
