using System.Collections.Generic;
using ControleOrcamentoAPI.Models;

namespace ControleOrcamentoAPI.DAO
{
    public class UsuarioDAO : DAO<Usuario>, IDAO<Usuario>
    {
        public Usuario Atualizar(Usuario entidade, UsuarioAutenticado token)
        {
            throw new System.NotImplementedException();
        }

        public Usuario BuscarPorID(long id)
        {
            throw new System.NotImplementedException();
        }

        public Usuario Criar(Usuario entidade, UsuarioAutenticado token)
        {
            throw new System.NotImplementedException();
        }

        public void Deletar(long id, UsuarioAutenticado token)
        {
            throw new System.NotImplementedException();
        }

        public IList<Usuario> ListarPorEntidade(Usuario entidade)
        {
            throw new System.NotImplementedException();
        }
    }
}
