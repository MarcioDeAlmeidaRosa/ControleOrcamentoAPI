using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ControleOrcamentoAPI.Models;
using ControleOrcamentoAPI.Exceptions;
using ControleOrcamentoAPI.Criptografia;
using System.Web.Script.Serialization;

namespace ControleOrcamentoAPI.DAO
{
    public class AuthDAO : DAO
    {
        private static int _LengthSalt = 0;

        static AuthDAO()
        {
            try
            {
                _LengthSalt = 32;
                int.TryParse(System.Configuration.ConfigurationSettings.AppSettings["LENGT_HSALT"].ToString(), out _LengthSalt);
            }
            catch (Exception)
            {
                _LengthSalt = 32;
            }
        }

        public UsuarioAutenticado Login(string usuario, string senha)
        {

            UsuarioAutenticado result;
            using (var cnn = new ConnectionFactory())
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT *       ");
                sql.AppendLine("  FROM USUARIO ");
                sql.AppendLine(" WHERE CONVERT(NVARCHAR(MAX), LOGIN) = @LOGIN ");
                cnn.AdicionarParametro("LOGIN", usuario);
                var dados = cnn.ObterDados(sql.ToString());
                if ((dados == null) || (dados.Rows == null) || (dados.Rows.Count < 1))
                    throw new RegistroNaoEncontradoException("Não encontrado registro com o filtro informado");
                JavaScriptSerializer js = new JavaScriptSerializer();
                byte[] SaltDeSerializado = js.Deserialize<byte[]>(Convert.ToString(dados.Rows[0]["SALT"]));
                Salt _Salt = new Salt(_LengthSalt);
                byte[] _senha = _Salt.GenerateDerivedKey(_LengthSalt, Encoding.UTF8.GetBytes(senha), SaltDeSerializado, 5000);
                if (Convert.ToString(dados.Rows[0]["SENHA"]) != _Salt.getPassword(_senha))
                    throw new RegistroNaoEncontradoException("Não encontrado registro com o filtro informado");
                result = MontarEntidade(dados.Rows[0]);
            }
            return result;
        }

        public void Registrar(Usuario entidade)
        {
            using (var cnn = new ConnectionFactory())
            {
                Salt _Salt = new Salt(_LengthSalt);
                JavaScriptSerializer js = new JavaScriptSerializer();
                byte[] SaltDeSerializado = _Salt.GenerateSalt();
                string SerializeSalt = js.Serialize(SaltDeSerializado);
                byte[] result = _Salt.GenerateDerivedKey(_LengthSalt, Encoding.UTF8.GetBytes(entidade.Senha), SaltDeSerializado, 5000);
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO          ");
                sql.AppendLine("    USUARIO          ");
                sql.AppendLine("           (         ");
                sql.AppendLine("            LOGIN ,  ");
                sql.AppendLine("            EMAIL ,  ");
                sql.AppendLine("            SENHA ,  ");
                sql.AppendLine("            SALT ,  ");
                sql.AppendLine("            ROLE     ");
                sql.AppendLine("           )         ");
                sql.AppendLine("    VALUES (         ");
                sql.AppendLine("            @LOGIN , ");
                sql.AppendLine("            @EMAIL , ");
                sql.AppendLine("            @SENHA , ");
                sql.AppendLine("            @SALT , ");
                sql.AppendLine("            'USER'   ");
                sql.AppendLine("           )         ");
                cnn.AdicionarParametro("LOGIN", entidade.Email);
                cnn.AdicionarParametro("EMAIL", entidade.Email);
                cnn.AdicionarParametro("SENHA", _Salt.getPassword(result));
                cnn.AdicionarParametro("SALT", SerializeSalt);
                try
                {
                    if (cnn.ExecutaComando(sql.ToString()) < 1) throw new RegistroNaoEncontradoException("Não encontrado registro com o filtro informado");
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627) throw new RegistroDuplicadoException("Usuário já existe na aplicação");
                    throw;
                }
            }
        }

        public UsuarioAutenticado ValidaToken(UsuarioAutenticado token)
        {
            throw new System.NotImplementedException();
        }

        private UsuarioAutenticado MontarEntidade(DataRow dado)
        {
            return new UsuarioAutenticado()
            {
                ID = Convert.ToInt64(dado["ID"]),
                Nome = Convert.ToString(dado["NOME"]),
                Email = Convert.ToString(dado["EMAIL"]),
                Role = Convert.ToString(dado["ROLE"]),
            };
        }
    }
}
