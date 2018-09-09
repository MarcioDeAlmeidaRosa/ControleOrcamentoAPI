using ControleOrcamentoAPI.DAO;
using ControleOrcamentoAPI.Models;

namespace ControleOrcamentoAPI.Orquestrador
{
    public class AuthOrquestrador : Orquestrador
    {
        private static AuthDAO _dao;

        static AuthOrquestrador()
        {
            _dao = new AuthDAO();
        }

        public UsuarioAutenticado Login(Usuario entidade)
        {
            return _dao.Login(entidade);
        }

        public void Registrar(Usuario entidade)
        {
            _dao.Registrar(entidade);
        }

        public UsuarioAutenticado ValidaToken(UsuarioAutenticado token)
        {
            return _dao.ValidaToken(token);
        }
    }
}
