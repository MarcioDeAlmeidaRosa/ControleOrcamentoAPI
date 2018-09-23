using ControleOrcamentoAPI.DAO;
using System.Collections.Generic;
using ControleOrcamentoAPI.Models;

namespace ControleOrcamentoAPI.Orquestrador
{
    public class UsuarioOrquestrador : Orquestrador, IOrquestrador<Usuario>
    {
        public Usuario Atualizar(long id, Usuario entidade, UsuarioAutenticado token)
        {
            using (var dao = new UsuarioDAO(token))
            {
                return dao.Atualizar(id, entidade);
            }
        }

        public Usuario BuscarPorID(long id, UsuarioAutenticado token)
        {
            using (var dao = new UsuarioDAO(token))
            {
                return dao.BuscarPorID(id);
            }
        }

        public Usuario Criar(Usuario entidade, UsuarioAutenticado token)
        {
            using (var dao = new UsuarioDAO(token))
            {
                return dao.Criar(entidade);
            }
        }

        public void Deletar(long id, UsuarioAutenticado token)
        {
            using (var dao = new UsuarioDAO(token))
            {
                dao.Deletar(id);
            }
        }

        public IList<Usuario> ListarPorEntidade(Usuario entidade, UsuarioAutenticado token)
        {
            using (var dao = new UsuarioDAO(token))
            {
                return dao.ListarPorEntidade(entidade);
            }
        }
    }
}
