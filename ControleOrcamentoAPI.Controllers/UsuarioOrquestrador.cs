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
            return _dao.Atualizar(entidade);
        }

        public Usuario BuscarPorID(long id, UsuarioAutenticado token)
        {
            return _dao.BuscarPorID(id);
        }

        public Usuario Criar(Usuario entidade, UsuarioAutenticado token)
        {
            return _dao.Criar(entidade);
        }

        public void Deletar(Usuario entidade, UsuarioAutenticado token)
        {
            _dao.Deletar(entidade);
        }

        public IList<Usuario> ListarPorEntidade(Usuario entidade, UsuarioAutenticado token)
        {
            return _dao.ListarPorEntidade(entidade);
        }
    }
}
