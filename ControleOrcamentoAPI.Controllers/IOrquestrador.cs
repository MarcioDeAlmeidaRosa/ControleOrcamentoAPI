using ControleOrcamentoAPI.Models;
using System.Collections.Generic;

namespace ControleOrcamentoAPI.Orquestrador
{
    public interface IOrquestrador<T> where T : class, new()
    {
        T BuscarPorID(long id);

        IList<T> ListarPorEntidade(T entidade);

        T Criar(T entidade, UsuarioAutenticado token);

        void Deletar(long id, UsuarioAutenticado token);

        T Atualizar(T entidade, UsuarioAutenticado token);
    }
}
