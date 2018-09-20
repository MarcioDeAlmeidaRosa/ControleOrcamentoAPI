using ControleOrcamentoAPI.DAO;
using System.Collections.Generic;
using ControleOrcamentoAPI.Models;

namespace ControleOrcamentoAPI.Orquestrador
{
    public class AgenciaOrquestrador : Orquestrador, IOrquestrador<Agencia>
    {
        public Agencia Atualizar(Agencia entidade, UsuarioAutenticado token)
        {
            using (var dao = new AgenciaDAO())
            {
                return dao.Atualizar(entidade, token);
            }
        }

        public Agencia BuscarPorID(long id)
        {
            using (var dao = new AgenciaDAO())
            {
                return dao.BuscarPorID(id);
            }
        }

        public Agencia Criar(Agencia entidade, UsuarioAutenticado token)
        {
            using (var dao = new AgenciaDAO())
            {
                return dao.Criar(entidade, token);
            }
        }

        public void Deletar(long id, UsuarioAutenticado token)
        {
            using (var dao = new AgenciaDAO())
            {
                dao.Deletar(id, token);
            }
        }

        public IList<Agencia> ListarPorEntidade(Agencia entidade)
        {
            using (var dao = new AgenciaDAO())
            {
                return dao.ListarPorEntidade(entidade);
            }
        }
    }
}
