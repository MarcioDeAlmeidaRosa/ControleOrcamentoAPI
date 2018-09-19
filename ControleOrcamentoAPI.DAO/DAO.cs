using System;
using System.Linq;
using ControleOrcamentoAPI.Models;
using ControleOrcamentoAPI.DataAccess;

namespace ControleOrcamentoAPI.DAO
{
    public abstract class DAO<T> : IDisposable where T: Entity
    {
        internal ControleOrcamentoAPIDataContext dbContext { get; }
        internal IQueryable<T> query = null;

        public DAO()
        {
            query = null;
            dbContext = new ControleOrcamentoAPIDataContext();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        internal protected IQueryable<T> AdicionarFiltrosComuns(Entity entidade)
        {
            if (entidade.ID > 0)
                query = from filtro in query where filtro.ID == entidade.ID select filtro;
            if (entidade.UsuarioInclusao.HasValue)
                query = from filtro in query where filtro.UsuarioInclusao == entidade.UsuarioInclusao.Value select filtro;
            if (entidade.DataInclusao.HasValue)
                query = from filtro in query where filtro.DataInclusao == entidade.DataInclusao.Value select filtro;
            if (entidade.UsuarioAlteracao.HasValue)
                query = from filtro in query where filtro.UsuarioAlteracao == entidade.UsuarioAlteracao.Value select filtro;
            if (entidade.DataAlteracao.HasValue)
                query = from filtro in query where filtro.DataAlteracao == entidade.DataAlteracao.Value select filtro;
            if (entidade.UsuarioCancelamento.HasValue)
                query = from filtro in query where filtro.UsuarioCancelamento == entidade.UsuarioCancelamento.Value select filtro;
            if (entidade.DataCancelamento.HasValue)
                query = from filtro in query where filtro.DataCancelamento == entidade.DataCancelamento.Value select filtro;
            return query;
        }
    }
}
