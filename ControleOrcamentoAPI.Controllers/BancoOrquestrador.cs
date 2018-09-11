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
            return _dao.Atualizar(entidade, token);
        }

        public Banco BuscarPorID(long id, UsuarioAutenticado token)
        {
            return _dao.BuscarPorID(id, token);
        }

        public Banco Criar(Banco entidade, UsuarioAutenticado token)
        {
            return _dao.Criar(entidade, token);
        }

        public void Deletar(Banco entidade, UsuarioAutenticado token)
        {
            _dao.Deletar(entidade, token);
        }

        public IList<Banco> ListarPorEntidade(Banco entidade, UsuarioAutenticado token)
        {
            return _dao.ListarPorEntidade(entidade, token);
        }
    }
}
