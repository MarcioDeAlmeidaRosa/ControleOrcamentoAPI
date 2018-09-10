using System;
using System.Data;
using System.Text;
using ControleOrcamentoAPI.Models;
using ControleOrcamentoAPI.Exceptions;

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
                    throw new NotFoundException("Não encontrado registro com o filtro informado");
                result = MontarEntidade(dados.Rows[0]);
            }
            return result;
        }

        public void Registrar(Usuario entidade)
        {
            throw new System.NotImplementedException();
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
