using System.Collections.Generic;
using ControleOrcamentoAPI.Models;

namespace ControleOrcamentoAPI.DAO
{
    public class UsuarioDAO : DAO, IDAO<Usuario>
    {
        public Usuario Atualizar(Usuario entidade, UsuarioAutenticado token)
        {
            throw new System.NotImplementedException();
        }

        public Usuario BuscarPorID(long id, UsuarioAutenticado token)
        {
            throw new System.NotImplementedException();
        }

        public Usuario Criar(Usuario entidade, UsuarioAutenticado token)
        {
            throw new System.NotImplementedException();
        }

        public void Deletar(Usuario entidade, UsuarioAutenticado token)
        {
            throw new System.NotImplementedException();
        }

        public IList<Usuario> ListarPorEntidade(Usuario entidade, UsuarioAutenticado token)
        {
            throw new System.NotImplementedException();
        }
    }
}
