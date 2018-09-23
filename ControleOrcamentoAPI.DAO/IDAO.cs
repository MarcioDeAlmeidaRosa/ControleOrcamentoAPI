using System;
using System.Collections.Generic;
using ControleOrcamentoAPI.Models;

namespace ControleOrcamentoAPI.DAO
{
    /// <summary>
    /// Definição do contrato do DAO
    /// </summary>
    public interface IDAO<T> where T : Entity, new()
    {
        /// <summary>
        /// Resposnável por atualizar o registro na entidade
        /// </summary>
        /// <param name="id"> ID da entidade para efetuar atualização no banco de dados</param>
        /// <param name="entidade"> Entidade contendo as informações que serão atualizadas no banco de dados</param>
        /// <returns>Entidade atualizada no banco de dados</returns>
        T Atualizar(long id, T entidade);

        /// <summary>
        /// Resposnável por pesquisar dados por ID
        /// </summary>
        /// <param name="id"> ID da entidade para esquisa no banco de dados</param>
        /// <returns>Entidade encontrada no banco de dados pelo ID informado</returns>
        T BuscarPorID(long id);

        /// <summary>
        /// Resposnável por incluir novo registro na entidade na coleção
        /// </summary>
        /// <param name="entidade"> Entidade contendo as informações que serão inseridas no banco de dados</param>
        /// <returns>Entidade incluída no banco de dados</returns>
        T Criar(T entidade);

        /// <summary>
        /// Resposnável por excluir logicamente o registro informado
        /// </summary>
        /// <param name="id"> Chave primária do registro no banco de dados</param>
        void Deletar(long id);

        /// <summary>
        /// Resposnável por pesquisar dados pelos filtros passados
        /// </summary>
        /// <param name="entidade"> Entidade contedo os filtros que serão considerados na consulta</param>
        /// <returns>Lista dos registros encontrados no banco de dados pelo filtro infomado</returns>
        IList<T> ListarPorEntidade(T entidade);
    }
}
