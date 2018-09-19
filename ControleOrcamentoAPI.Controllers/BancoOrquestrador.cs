using ControleOrcamentoAPI.DAO;
using System.Collections.Generic;
using ControleOrcamentoAPI.Models;

namespace ControleOrcamentoAPI.Orquestrador
{
    public class BancoOrquestrador : Orquestrador, IOrquestrador<Banco>
    {
        public Banco Atualizar(Banco entidade, UsuarioAutenticado token)
        {
            using (var dao = new BancoDAO())
            {
                return dao.Atualizar(entidade, token);
            }
        }

        public Banco BuscarPorID(long id)
        {
            using (var dao = new BancoDAO())
            {
                return dao.BuscarPorID(id);
            }
        }

        public Banco Criar(Banco entidade, UsuarioAutenticado token)
        {
            using (var dao = new BancoDAO())
            {
                return dao.Criar(entidade, token);
            }
        }

        public void Deletar(long id, UsuarioAutenticado token)
        {
            using (var dao = new BancoDAO())
            {
                dao.Deletar(id, token);
            }
        }

        public IList<Banco> ListarPorEntidade(Banco entidade)
        {
            using (var dao = new BancoDAO())
            {
                return dao.ListarPorEntidade(entidade);
            }
        }
    }
}
