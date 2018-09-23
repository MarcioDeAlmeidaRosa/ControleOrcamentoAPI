using System.Collections.Generic;
using ControleOrcamentoAPI.Models;

namespace ControleOrcamentoAPI.Orquestrador
{
    /// <summary>
    /// Definição do contrato do orquestrador
    /// </summary>
    /// <typeparam name="T">Entidade que assumirá no generic</typeparam>
    public interface IOrquestrador<T> where T : Entity, new()
    {
        /// <summary>
        /// Definição do método que deve ser implementado pela classe concreta ao ser implementada, método responsável por recuperar 
        /// a entidade informada no tipo T por id
        /// </summary>
        /// <param name="id">Chave primária do registro na entidade informada no tipo T</param>
        /// <param name="token"> Usuário logado na aplicação</param>
        /// <returns>Objeto encontrado pelo id informado do tipo T</returns>
        T BuscarPorID(long id, UsuarioAutenticado token);

        IList<T> ListarPorEntidade(T entidade, UsuarioAutenticado token);

        T Criar(T entidade, UsuarioAutenticado token);

        void Deletar(long id, UsuarioAutenticado token);

        T Atualizar(long id, T entidade, UsuarioAutenticado token);
    }
}
