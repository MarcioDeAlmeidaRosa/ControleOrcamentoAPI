using ControleOrcamentoAPI.DAO;
using System.Collections.Generic;
using ControleOrcamentoAPI.Models;

namespace ControleOrcamentoAPI.Orquestrador
{
    public class UsuarioOrquestrador : Orquestrador, IOrquestrador<Usuario>
    {
        private static UsuarioDAO _dao;

        static UsuarioOrquestrador()
        {
            _dao = new UsuarioDAO();
        }

        public Usuario Atualizar(Usuario entidade, UsuarioAutenticado token)
        {
            return _dao.Atualizar(entidade, token);
        }

        public Usuario BuscarPorID(long id, UsuarioAutenticado token)
        {
            return _dao.BuscarPorID(id, token);
        }

        public Usuario Criar(Usuario entidade, UsuarioAutenticado token)
        {
            return _dao.Criar(entidade, token);
        }

        public void Deletar(Usuario entidade, UsuarioAutenticado token)
        {
            _dao.Deletar(entidade, token);
        }

        public IList<Usuario> ListarPorEntidade(Usuario entidade, UsuarioAutenticado token)
        {
            return _dao.ListarPorEntidade(entidade, token);
        }
    }
}
