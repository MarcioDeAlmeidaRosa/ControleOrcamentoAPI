using ControleOrcamentoAPI.DAO;
using System.Collections.Generic;
using ControleOrcamentoAPI.Models;

namespace ControleOrcamentoAPI.Orquestrador
{
    public class BancoOrquestrador : Orquestrador, IOrquestrador<Banco>
    {
        private static BancoDAO _dao;

        static BancoOrquestrador()
        {
            _dao = new BancoDAO();
        }

        public Banco Atualizar(Banco entidade, UsuarioAutenticado token)
        {
            return _dao.Atualizar(entidade);
        }

        public Banco BuscarPorID(long id, UsuarioAutenticado token)
        {
            return _dao.BuscarPorID(id);
        }

        public Banco Criar(Banco entidade, UsuarioAutenticado token)
        {
            return _dao.Criar(entidade);
        }

        public void Deletar(Banco entidade, UsuarioAutenticado token)
        {
            _dao.Deletar(entidade);
        }

        public IList<Banco> ListarPorEntidade(Banco entidade, UsuarioAutenticado token)
        {
            return _dao.ListarPorEntidade(entidade);
        }
    }
}
