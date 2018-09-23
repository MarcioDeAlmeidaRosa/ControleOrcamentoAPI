using ControleOrcamentoAPI.DAO;
using System.Collections.Generic;
using ControleOrcamentoAPI.Models;

namespace ControleOrcamentoAPI.Orquestrador
{
    public class AgenciaOrquestrador : Orquestrador, IOrquestrador<Agencia>
    {
        public Agencia Atualizar(long id, Agencia entidade, UsuarioAutenticado token)
        {
            using (var dao = new AgenciaDAO(token))
            {
                return dao.Atualizar(id, entidade);
            }
        }

        public Agencia BuscarPorID(long id, UsuarioAutenticado token)
        {
            using (var dao = new AgenciaDAO(token))
            {
                return dao.BuscarPorID(id);
            }
        }

        public Agencia Criar(Agencia entidade, UsuarioAutenticado token)
        {
            using (var dao = new AgenciaDAO(token))
            {
                return dao.Criar(entidade);
            }
        }

        public void Deletar(long id, UsuarioAutenticado token)
        {
            using (var dao = new AgenciaDAO(token))
            {
                dao.Deletar(id);
            }
        }

        public IList<Agencia> ListarPorEntidade(Agencia entidade, UsuarioAutenticado token)
        {
            using (var dao = new AgenciaDAO(token))
            {
                return dao.ListarPorEntidade(entidade);
            }
        }
    }
}
