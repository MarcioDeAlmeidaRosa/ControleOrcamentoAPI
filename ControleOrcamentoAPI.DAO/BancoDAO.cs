using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using ControleOrcamentoAPI.Models;
using ControleOrcamentoAPI.Exceptions;

namespace ControleOrcamentoAPI.DAO
{
    public class BancoDAO : DAO, IDAO<Banco>
    {
        public Banco Atualizar(Banco entidade)
        {
            throw new System.NotImplementedException();
        }

        public Banco BuscarPorID(long id)
        {
            Banco result = null;
            using (var cnn = new ConnectionFactory())
            {
                var sql = @"SELECT * 
                              FROM BANCO 
                             WHERE ID = @ID
                           ";
                cnn.AdicionarParametro("ID", id);
                var dados = cnn.ObterDados(sql);
                if ((dados == null) || (dados.Rows == null) || (dados.Rows.Count < 1))
                    throw new NotFoundException("Não encontrado registro com o filtro informado");

                result = MontarEntidade(dados.Rows[0]);
            }
            return result;
        }

        public Banco Criar(Banco entidade)
        {
            throw new System.NotImplementedException();
        }

        public void Deletar(Banco entidade)
        {
            throw new System.NotImplementedException();
        }

        public IList<Banco> ListarPorEntidade(Banco entidade)
        {
            IList<Banco> result = null;
            using (var cnn = new ConnectionFactory())
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT *     ");
                sql.AppendLine("  FROM BANCO ");
                sql.AppendLine(" WHERE 1 = 1 ");
                if (entidade != null)
                {
                    if (!string.IsNullOrWhiteSpace(entidade.Codigo))
                    {
                        sql.AppendLine(" AND CONVERT(NVARCHAR(MAX), CODIGO) = @CODIGO ");
                        cnn.AdicionarParametro("CODIGO", entidade.Codigo);
                    }

                    if (!string.IsNullOrWhiteSpace(entidade.Nome))
                    {
                        sql.AppendLine(" AND CONVERT(NVARCHAR(MAX), NOME) LIKE @NOME ");
                        cnn.AdicionarParametro("NOME", string.Format("%{0}%", entidade.Nome));
                    }
                }
                var dados = cnn.ObterDados(sql.ToString());
                if ((dados == null) || (dados.Rows == null) || (dados.Rows.Count < 1))
                    throw new NotFoundException("Não encontrado registro com o filtro informado");

                result = new List<Banco>();
                foreach (DataRow dado in dados.Rows)
                    result.Add(MontarEntidade(dado));
            }
            return result;
        }

        private Banco MontarEntidade(DataRow dado)
        {
            return new Banco()
            {
                ID = Convert.ToInt64(dado["ID"]),
                Nome = Convert.ToString(dado["NOME"]),
                Codigo = Convert.ToString(dado["CODIGO"]),
            };
        }
    }
}
