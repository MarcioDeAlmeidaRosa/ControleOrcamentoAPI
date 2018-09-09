using System.Collections.Generic;

namespace ControleOrcamentoAPI.DAO
{
    public interface IDAO<T> where T : class, new()
    {
        T BuscarPorID(long id);

        IList<T> ListarPorEntidade(T entidade);

        T Criar(T entidade);

        void Deletar(T entidade);

        T Atualizar(T entidade);
    }
}
