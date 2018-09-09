using System.Collections.Generic;

namespace ControleOrcamentoAPI.Orquestrador
{
    public abstract class Orquestrador<T> where T : class, new()
    {
        public abstract T BuscarPorID(long id);

        public abstract IList<T> ListarPorEntidade(T entidade);

        public abstract T Criar(T entidade);

        public abstract void Deletar(T entidade);

        public abstract T Atualizar(T entidade);
    }
}
