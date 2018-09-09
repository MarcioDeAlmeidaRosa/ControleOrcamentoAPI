using ControleOrcamentoAPI.Models;
using System.Collections.Generic;

namespace ControleOrcamentoAPI.Orquestrador
{
    public interface IOrquestrador<T> where T : class, new()
    {
        T BuscarPorID(long id, UsuarioAutenticado token);

        IList<T> ListarPorEntidade(T entidade, UsuarioAutenticado token);

        T Criar(T entidade, UsuarioAutenticado token);

        void Deletar(T entidade, UsuarioAutenticado token);

        T Atualizar(T entidade, UsuarioAutenticado token);
    }
}
