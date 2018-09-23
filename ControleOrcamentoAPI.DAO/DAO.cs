using System;
using System.Linq;
using ControleOrcamentoAPI.Models;
using ControleOrcamentoAPI.Extensoes;
using ControleOrcamentoAPI.DataAccess;

namespace ControleOrcamentoAPI.DAO
{
    /// <summary>
    /// Funcionalidades comuns entres dos DAOs
    /// </summary>
    public abstract class DAO<T> : IDisposable where T : Entity
    {
        protected Func<T, T> FuncAjustaTimeZone { get; set; }

        /// <summary>
        /// Propriedade de acesso ao usuário logado
        /// </summary>
        protected UsuarioAutenticado Token { get; }
        
        /// <summary>
        /// Cria instância do contexto com o banco de dados
        /// </summary>
        public DAO()
        {
            query = null;
            _dbContext = new ControleOrcamentoAPIDataContext();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="ArgumentNullException"> Excação lançada quando o <paramref name="token"/> é nulo</exception>
        public DAO(UsuarioAutenticado token) : this()
        {
            Token = token ?? throw new ArgumentNullException("Não informado o usuário Logado na aplicação", nameof(token));
            Configurar();
        }

        /// <summary>
        /// Armazena o contexto eentity da aplicação
        /// </summary>
        internal ControleOrcamentoAPIDataContext _dbContext { get; }

        /// <summary>
        /// Armazena os critérios das querys
        /// </summary>
        internal IQueryable<T> query = null;

        /// <summary>
        /// Responsável por encerrar a conexão com o banco despois que o DAO é utilizado
        /// </summary>
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        /// <summary>
        /// Trava retornos quando usuário não for ADM
        /// </summary>
        /// <returns></returns>
        internal protected IQueryable<T> VerificaUsuarioAdmin()
        {
            if (Token.Claim != "ADMIN")
            {
                query = from filtro in query where filtro.UsuarioInclusao == Token.ID select filtro;

                query = from filtro in query where filtro.DataCancelamento == null select filtro;
            }

            return query;
        }

        /// <summary>
        /// Responsável por adicionar filtros comuns à querys de cada DAO
        /// </summary>
        /// <param name="entidade"> Entidade contendo os parâmetros preenchidos pela interface</param>
        /// <param name="timeZone"> TimeZone do usuário logada no aplicação</param>
        /// <returns>Retorna novas condições para execução da consulta no banco quando estes são preenchidos</returns>
        internal protected IQueryable<T> AdicionarFiltrosComuns(Entity entidade)
        {
            query = VerificaUsuarioAdmin();

            if (entidade.ID > 0)
                query = from filtro in query where filtro.ID == entidade.ID select filtro;
            if (entidade.UsuarioInclusao.HasValue)
                query = from filtro in query where filtro.UsuarioInclusao == entidade.UsuarioInclusao.Value select filtro;
            if (entidade.DataInclusao.HasValue)
                query = from filtro in query where filtro.DataInclusao == entidade.DataInclusao.Value.ToTimeZoneTime(Token.TimeZone) select filtro;
            if (entidade.UsuarioAlteracao.HasValue)
                query = from filtro in query where filtro.UsuarioAlteracao == entidade.UsuarioAlteracao.Value select filtro;
            if (entidade.DataAlteracao.HasValue)
                query = from filtro in query where filtro.DataAlteracao == entidade.DataAlteracao.Value.ToTimeZoneTime(Token.TimeZone) select filtro;
            if (entidade.UsuarioCancelamento.HasValue)
                query = from filtro in query where filtro.UsuarioCancelamento == entidade.UsuarioCancelamento.Value select filtro;
            if (entidade.DataCancelamento.HasValue)
                query = from filtro in query where filtro.DataCancelamento == entidade.DataCancelamento.Value.ToTimeZoneTime(Token.TimeZone) select filtro;
            return query;
        }

        /// <summary>
        /// Responsavel por definir a função que deverá ser executada para ajustar dados de data e hora conforme Time Zone do usuário
        /// </summary>
        protected abstract void Configurar();
    }
}
