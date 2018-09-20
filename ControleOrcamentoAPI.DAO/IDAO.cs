using System.Collections.Generic;
using ControleOrcamentoAPI.Models;

namespace ControleOrcamentoAPI.DAO
{
    /// <summary>
    /// Interface de contrato dos métodos de um DAO
    /// </summary>
    public interface IDAO<T> where T : Entity, new()
    {
        /// <summary>
        /// Definição do método que deve ser implementado pela classe concreta ao ser implementada, método resposnável por pesquisar dados por ID
        /// </summary>
        /// <param name="id">Chave primária do registro no banco de dados</param>
        /// <returns>Entidade encontrada no banco de dados pelo ID informado</returns>
        T BuscarPorID(long id);

        /// <summary>
        /// Definição do método que deve ser implementado pela classe concreta ao ser implementada, método resposnável por pesquisar dados pelos filtros passados
        /// </summary>
        /// <param name="entidade">Entidade contedo os filtros que serão considerados na consulta</param>
        /// <returns>Lista dos registros encontrados no banco de dados pelo filtro infomado</returns>
        IList<T> ListarPorEntidade(T entidade);

        /// <summary>
        /// Definição do método que deve ser implementado pela classe concreta ao ser implementada, método resposnável por incluir novo registro na entidade informada
        /// </summary>
        /// <param name="entidade">Entidade contendo as informações que serão inseridas no banco de dados</param>
        /// <param name="token">Usuário logado na aplicação</param>
        /// <returns>Entidade incluída no banco de dados</returns>
        T Criar(T entidade, UsuarioAutenticado token);

        /// <summary>
        /// Definição do método que deve ser implementado pela classe concreta ao ser implementada, método resposnável por excluir logicamente o registro informado
        /// </summary>
        /// <param name="id">Chave primária do registro no banco de dados</param>
        /// <param name="token">Usuário logado na aplicação</param>
        void Deletar(long id, UsuarioAutenticado token);

        /// <summary>
        /// Definição do método que deve ser implementado pela classe concreta ao ser implementada, método resposnável por atualizar o registro na entidade informada
        /// </summary>
        /// <param name="entidade">Entidade contendo as informações que serão atualizadas no banco de dados</param>
        /// <param name="token">Usuário logado na aplicação</param>
        /// <returns>Entidade atualizada no banco de dados</returns>
        T Atualizar(T entidade, UsuarioAutenticado token);
    }
}
