using ControleOrcamentoAPI.DAO;
using System.Collections.Generic;
using ControleOrcamentoAPI.Models;

namespace ControleOrcamentoAPI.Orquestrador
{
    public class UsuarioOrquestrador : Orquestrador, IOrquestrador<Usuario>
    {
        public Usuario Atualizar(Usuario entidade, UsuarioAutenticado token)
        {
            using (var dao = new UsuarioDAO())
            {
                return dao.Atualizar(entidade, token);
            }
        }

        public Usuario BuscarPorID(long id)
        {
            using (var dao = new UsuarioDAO())
            {
                return dao.BuscarPorID(id);
            }
        }

        public Usuario Criar(Usuario entidade, UsuarioAutenticado token)
        {
            using (var dao = new UsuarioDAO())
            {
                return dao.Criar(entidade, token);
            }
        }

        public void Deletar(long id, UsuarioAutenticado token)
        {
            using (var dao = new UsuarioDAO())
            {
                dao.Deletar(id, token);
            }
        }

        public IList<Usuario> ListarPorEntidade(Usuario entidade)
        {
            using (var dao = new UsuarioDAO())
            {
                return dao.ListarPorEntidade(entidade);
            }
        }
    }
}
