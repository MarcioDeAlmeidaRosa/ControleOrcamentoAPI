using System.Collections.Generic;
using System.Threading.Tasks;
using ControleOrcamentoAPI.Models;

namespace ControleOrcamentoAPI.DAO
{
    /// <summary>
    /// Interface de definição dos métodos obrigatórios em um DAO
    /// </summary>
    /// <typeparam name="T">Entidade que será utilizada no lugar do T</typeparam>
    public interface IDAO<T> where T : Entity, new()
    {
        /// <summary>
        /// Resposnável por atualizar o registro na entidade do tipo <typeparamref name="T"/>
        /// </summary>
        /// <param name="id">ID do registro da entidade informada no tipo <typeparamref name="T"/></param>
        /// <param name="entidade"> Entidade do tipo <typeparamref name="T"/> contendo as informações que serão atualizadas no banco de dados</param>
        /// <returns>Entidade atualizada no banco de dados</returns>
        T Atualizar(long id, T entidade);

        /// <summary>
        /// Responsável por recuperar a entidade definida no tipo <typeparamref name="T"/> pelo ID
        /// </summary>
        /// <param name="id">ID do registro da entidade informada no tipo <typeparamref name="T"/></param>
        /// <returns>Objeto do tipo <typeparamref name="T"/> encontrado pelo id informado</returns>
        T BuscarPorID(long id);

        /// <summary>
        /// Resposnável por incluir novo registro na entidade na coleção do tipo <typeparamref name="T"/>
        /// </summary>
        /// <param name="entidade"> Entidade do tipo <typeparamref name="T"/> contendo as informações que serão inseridas no banco de dados</param>
        /// <returns>Entidade do tipo <typeparamref name="T"/> incluída no banco de dados</returns>
        T Criar(T entidade);

        /// <summary>
        /// Resposnável por excluir logicamente o registro informado
        /// </summary>
        /// <param name="id">ID do registro da entidade informada no tipo <typeparamref name="T"/></param>
        /// <returns>Task para chamada assíncrona</returns>
        Task Deletar(long id);

        /// <summary>
        /// Responsável por recuperar uma lista da entidade definida no tipo <typeparamref name="T"/> pelo campos da própria entidade
        /// </summary>
        /// <param name="entidade"> Entidade do tipo <typeparamref name="T"/> contendo as informações que serão utilizadas para filtrar os dados</param>
        /// <returns>Objetos encontrado pelo filtro informado do tipo <typeparamref name="T"/></returns>
        IList<T> ListarPorEntidade(T entidade);
    }
}
