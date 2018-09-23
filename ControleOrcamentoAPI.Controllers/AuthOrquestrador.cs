using ControleOrcamentoAPI.DAO;
using ControleOrcamentoAPI.Models;

namespace ControleOrcamentoAPI.Orquestrador
{
    public class AuthOrquestrador : Orquestrador
    {
        public UsuarioAutenticado Login(string usuario, string senha)
        {
            //TODO: REMOVER new UsuarioAutenticado()
            using (var dao = new AuthDAO())
            {
                return dao.Login(usuario, senha);
            }
        }

        public void Registrar(Usuario entidade)
        {
            //TODO: REMOVER new UsuarioAutenticado()
            using (var dao = new AuthDAO())
            {
                dao.Registrar(entidade);
            }
        }

        public UsuarioAutenticado ValidaToken(UsuarioAutenticado token)
        {
            //TODO: REMOVER new UsuarioAutenticado()
            using (var dao = new AuthDAO())
            {
                return dao.ValidaToken(token);
            }
        }
    }
}
