using System;
using System.Linq;
using System.Data;
using System.Text;
using System.Configuration;
using ControleOrcamentoAPI.Models;
using System.Data.Entity.Validation;
using ControleOrcamentoAPI.Exceptions;
using System.Web.Script.Serialization;
using ControleOrcamentoAPI.Criptografia;

namespace ControleOrcamentoAPI.DAO
{
    /// <summary>
    /// Classe responsável por manter dados pásico de usuário
    /// </summary>
    public class AuthDAO : DAO<Usuario>
    {
        private static int _LengthSalt = 0;

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
        /// Método responsável por efetuar o login do usuário na aplicação
        /// </summary>
        /// <param name="login">Usuário de autenticação</param>
        /// <param name="senha">Senha do usuário para autenticação</param>
        /// <returns>Informações básicas para autenticar o usuário</returns>
        public UsuarioAutenticado Login(string login, string senha)
        {
            var entidadeLocalizada = dbContext.Usuarios.Where(data => data.Login.Equals(login, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            if (entidadeLocalizada == null)
                throw new RegistroNaoEncontradoException("Usuário não localizado.");

            JavaScriptSerializer js = new JavaScriptSerializer();
            byte[] SaltDeSerializado = js.Deserialize<byte[]>(entidadeLocalizada.Salt);
            Salt _Salt = new Salt(_LengthSalt);
            byte[] _senha = _Salt.GenerateDerivedKey(_LengthSalt, Encoding.UTF8.GetBytes(senha), SaltDeSerializado, 5000);
            if (entidadeLocalizada.Senha != _Salt.getPassword(_senha))
                throw new RegistroNaoEncontradoException("Usuário não localizado.");

            if (!entidadeLocalizada.Verificado)
                throw new UsuarioNaoVerificadoException("Usuário não verificado.");

            if (entidadeLocalizada.Bloqueado)
                throw new UsuarioBloqueadoException("Usuário bloqueado.");

            return new UsuarioAutenticado()
            {
                ID = entidadeLocalizada.ID,
                Nome = entidadeLocalizada.Nome,
                Email = entidadeLocalizada.Email,
                Claim = entidadeLocalizada.Claim
            };
        }

        /// <summary>
        /// Método responsável por efetuar o registro de novos usuários na aplicação
        /// </summary>
        /// <param name="entidade">Entidade contendo informações do novo usuário à ser registrado</param>
        public void Registrar(Usuario entidade)
        {
            try
            {
                var entidadeLocalizada = dbContext.Usuarios.Where(data => data.Login.Equals(entidade.Login, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
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
                entidade.Bloqueado = true;
                entidade.DataBloqueio = DateTime.Now;
                entidade.Verificado = false;
                dbContext.Usuarios.Add(entidade);
                dbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder st = new StringBuilder();
                ex.EntityValidationErrors.ToList().ForEach(errs => errs.ValidationErrors.ToList().ForEach(err => st.AppendLine(err.ErrorMessage)));
                //TODO AJUSTAR O EXCEPTION DO RETORNO
                throw new Exception(st.ToString(), ex);
            }
        }

        /// <summary>
        /// Método responsável por efetuar a validação do token gerado
        /// </summary>
        /// <param name="token">Usuário logado na aplicação</param>
        /// <returns></returns>
        public UsuarioAutenticado ValidaToken(UsuarioAutenticado token)
        {
            throw new System.NotImplementedException();
        }
    }
}
