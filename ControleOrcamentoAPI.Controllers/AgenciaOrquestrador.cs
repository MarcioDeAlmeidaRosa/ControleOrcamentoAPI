﻿using ControleOrcamentoAPI.DAO;
using System.Collections.Generic;
using ControleOrcamentoAPI.Models;

namespace ControleOrcamentoAPI.Orquestrador
{
    /// <summary>
    /// Responsável por Orquestrar Agência
    /// </summary>
    public class AgenciaOrquestrador : Orquestrador, IOrquestrador<Agencia>
    {
        /// <summary>
        /// Resposnável por atualizar o registro na entidade do tipo Agencia
        /// </summary>
        /// <param name="id">ID do registro da entidade informada no tipo Agencia</param>
        /// <param name="entidade"> Entidade do tipo Agencia contendo as informações que serão atualizadas no banco de dados</param>
        /// <param name="token"> Usuário logado na aplicação</param>
        /// <returns>Entidade atualizada no banco de dados</returns>
        public Agencia Atualizar(long id, Agencia entidade, UsuarioAutenticado token)
        {
            using (var dao = new AgenciaDAO(token))
            {
                return dao.Atualizar(id, entidade);
            }
        }

        /// <summary>
        /// Responsável por recuperar a entidade definida no tipo Agencia pelo ID
        /// </summary>
        /// <param name="id">ID do registro da entidade informada no tipo Agencia</param>
        /// <param name="token"> Usuário logado na aplicação</param>
        /// <returns>Objeto do tipo Agencia encontrado pelo id informado</returns>
        public Agencia BuscarPorID(long id, UsuarioAutenticado token)
        {
            using (var dao = new AgenciaDAO(token))
            {
                return dao.BuscarPorID(id);
            }
        }

        /// <summary>
        /// Resposnável por incluir novo registro na entidade na coleção do tipo Agencia
        /// </summary>
        /// <param name="entidade"> Entidade do tipo Agencia contendo as informações que serão inseridas no banco de dados</param>
        /// <param name="token"> Usuário logado na aplicação</param>
        /// <returns>Entidade do tipo Agencia incluída no banco de dados</returns>
        public Agencia Criar(Agencia entidade, UsuarioAutenticado token)
        {
            using (var dao = new AgenciaDAO(token))
            {
                return dao.Criar(entidade);
            }
        }

        /// <summary>
        /// Resposnável por excluir logicamente o registro informado
        /// </summary>
        /// <param name="id">ID do registro da entidade informada no tipo Agencia</param>
        /// <param name="token"> Usuário logado na aplicação</param>
        public void Deletar(long id, UsuarioAutenticado token)
        {
            using (var dao = new AgenciaDAO(token))
            {
                dao.Deletar(id);
            }
        }

        /// <summary>
        /// Responsável por recuperar uma lista da entidade definida no tipo Agencia pelo campos da própria entidade
        /// </summary>
        /// <param name="entidade"> Entidade do tipo Agencia contendo as informações que serão utilizadas para filtrar os dados</param>
        /// <param name="token"> Usuário logado na aplicação</param>
        /// <returns>Objetos encontrado pelo filtro informado do tipo Agencia</returns>
        public IList<Agencia> ListarPorEntidade(Agencia entidade, UsuarioAutenticado token)
        {
            using (var dao = new AgenciaDAO(token))
            {
                return dao.ListarPorEntidade(entidade);
            }
        }
    }
}
