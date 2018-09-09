using ControleOrcamentoAPI.DAO;
using System.Collections.Generic;
using ControleOrcamentoAPI.Models;

namespace ControleOrcamentoAPI.Orquestrador
{
    public class BancoOrquestrador : Orquestrador<Banco>
    {
        private static BancoDAO _dao;

        static BancoOrquestrador()
        {
            _dao = new BancoDAO();
        }

        public override Banco Atualizar(Banco entidade)
        {
            return _dao.Atualizar(entidade);
        }

        public override Banco BuscarPorID(long id)
        {
            return _dao.BuscarPorID(id);
        }

        public override Banco Criar(Banco entidade)
        {
            return _dao.Criar(entidade);
        }

        public override void Deletar(Banco entidade)
        {
            _dao.Deletar(entidade);
        }

        public override IList<Banco> ListarPorEntidade(Banco entidade)
        {
            return _dao.ListarPorEntidade(entidade);
        }
    }
}
