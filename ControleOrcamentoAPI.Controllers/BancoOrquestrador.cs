using ControleOrcamentoAPI.DAO;
using System.Collections.Generic;
using ControleOrcamentoAPI.Models;

namespace ControleOrcamentoAPI.Orquestrador
{
    public class BancoOrquestrador : Orquestrador, IOrquestrador<Banco>
    {
        public Banco Atualizar(long id, Banco entidade, UsuarioAutenticado token)
        {
            using (var dao = new BancoDAO(token))
            {
                return dao.Atualizar(id, entidade);
            }
        }

        public Banco BuscarPorID(long id, UsuarioAutenticado token)
        {
            using (var dao = new BancoDAO(token))
            {
                return dao.BuscarPorID(id);
            }
        }

        public Banco Criar(Banco entidade, UsuarioAutenticado token)
        {
            using (var dao = new BancoDAO(token))
            {
                return dao.Criar(entidade);
            }
        }

        public void Deletar(long id, UsuarioAutenticado token)
        {
            using (var dao = new BancoDAO(token))
            {
                dao.Deletar(id);
            }
        }

        public IList<Banco> ListarPorEntidade(Banco entidade, UsuarioAutenticado token)
        {
            using (var dao = new BancoDAO(token))
            {
                return dao.ListarPorEntidade(entidade);
            }
        }
    }
}
