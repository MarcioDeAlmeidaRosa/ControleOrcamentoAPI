using System;
using System.Collections.Generic;
using ControleOrcamentoAPI.Exceptions;
using ControleOrcamentoAPI.Models;

namespace ControleOrcamentoAPI.DAO
{
    public class BancoDAO : DAO<Banco>
    {
        public override Banco Atualizar(Banco entidade)
        {
            throw new System.NotImplementedException();
        }

        public override Banco BuscarPorID(long id)
        {
            Banco result = null;
            using (var cnn = new ConnectionFactory())
            {
                var sql = @"SELECT * 
                              FROM BANCO 
                             WHERE ID = @ID
                           ";

                var dados = cnn.ObterDados(sql, new[] { cnn.ObterParametro("ID", id) });
                if ((dados == null) || (dados.Rows == null) || (dados.Rows.Count < 1))
                    throw new NotFoundException("Não encontrado registro com o filtro informado");

                result = new Banco()
                {
                    ID = Convert.ToInt64(dados.Rows[0]["ID"]),
                    Nome = Convert.ToString(dados.Rows[0]["NOME"]),
                    Codigo = Convert.ToString(dados.Rows[0]["CODIGO"]),
                };
            }
            return result;
        }

        public override Banco Criar(Banco entidade)
        {
            throw new System.NotImplementedException();
        }

        public override void Deletar(Banco entidade)
        {
            throw new System.NotImplementedException();
        }

        public override IList<Banco> ListarPorEntidade(Banco entidade)
        {
            return new List<Banco>() { new Banco() { ID = 1, Codigo = "237", Nome = "Bradesco" } };
        }
    }
}
