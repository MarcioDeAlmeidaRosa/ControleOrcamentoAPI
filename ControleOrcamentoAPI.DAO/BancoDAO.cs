using System.Collections.Generic;
using ControleOrcamentoAPI.Models;

namespace ControleOrcamentoAPI.DAO
{
    public class BancoDAO : DAO<Banco>
    {
        public BancoDAO()
        {
        }

        public override Banco Atualizar(Banco entidade)
        {
            throw new System.NotImplementedException();
        }

        public override Banco BuscarPorID(long id)
        {
            return new Banco() { ID = 1, Codigo = "237", Nome = "Bradesco" };
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
