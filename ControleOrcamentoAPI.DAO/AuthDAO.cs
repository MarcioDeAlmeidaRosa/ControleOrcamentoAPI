using System;
using System.Data;
using System.Text;
using ControleOrcamentoAPI.Models;
using ControleOrcamentoAPI.Exceptions;
using System.Data.SqlClient;

namespace ControleOrcamentoAPI.DAO
{
    public class AuthDAO : DAO
    {
        public UsuarioAutenticado Login(string usuario, string senha)
        {
            UsuarioAutenticado result;
            using (var cnn = new ConnectionFactory())
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT *       ");
                sql.AppendLine("  FROM USUARIO ");
                sql.AppendLine(" WHERE CONVERT(NVARCHAR(MAX), LOGIN) = @LOGIN ");
                sql.AppendLine("   AND CONVERT(NVARCHAR(MAX), SENHA) = @SENHA ");
                cnn.AdicionarParametro("LOGIN", usuario);
                cnn.AdicionarParametro("SENHA", senha);
                var dados = cnn.ObterDados(sql.ToString());
                if ((dados == null) || (dados.Rows == null) || (dados.Rows.Count < 1))
                    throw new RegistroNaoEncontradoException("Não encontrado registro com o filtro informado");
                result = MontarEntidade(dados.Rows[0]);
            }
            return result;
        }

        public void Registrar(Usuario entidade)
        {
            using (var cnn = new ConnectionFactory())
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("INSERT INTO          ");
                sql.AppendLine("    USUARIO          ");
                sql.AppendLine("           (         ");
                sql.AppendLine("            LOGIN ,  ");
                sql.AppendLine("            EMAIL ,  ");
                sql.AppendLine("            SENHA ,  ");
                sql.AppendLine("            ROLE     ");
                sql.AppendLine("           )         ");
                sql.AppendLine("    VALUES (         ");
                sql.AppendLine("            @LOGIN , ");
                sql.AppendLine("            @EMAIL , ");
                sql.AppendLine("            @SENHA , ");
                sql.AppendLine("            'USER'   ");
                sql.AppendLine("           )         ");
                cnn.AdicionarParametro("LOGIN", entidade.Email);
                cnn.AdicionarParametro("EMAIL", entidade.Email);
                cnn.AdicionarParametro("SENHA", entidade.Senha);
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
