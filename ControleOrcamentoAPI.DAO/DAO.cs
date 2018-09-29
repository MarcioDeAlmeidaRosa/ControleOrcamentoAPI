using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using ControleOrcamentoAPI.Models;
using ControleOrcamentoAPI.Extensoes;
using ControleOrcamentoAPI.DataAccess;
using System.Collections.Generic;

namespace ControleOrcamentoAPI.DAO
{
    /// <summary>
    /// Classe que fornece o acesso ao bando de dados para a aplicação
    /// </summary>
    /// <typeparam name="T">Entidade que assumirá no lugar do T</typeparam>
    public abstract class DAO<T> : IDisposable where T : Entity
    {
        //internal protected Expression<Func<IQueryable<T>, bool>> FiltrosEspecializados;

        /// <summary>
        /// Propriedade que irá armazenar a função para transfomar as datas em formato UTC no formato da Time Zone do usuário
        /// </summary>
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
        /// Efetua a pesquisa da entidade por ID
        /// </summary>
        /// <param name="id">Chave primária enviada para pesquisa</param>
        /// <param name="manterContexto">Controle para manter o retorno localizado no cache do Entity ou náo</param>
        /// <returns>Entidade localizada pelo <paramref name="id"/> informado</returns>
        internal protected async Task<T> FiltrarPorID(long id, bool manterContexto = false)
        {
            var obj = (T)Activator.CreateInstance(typeof(T));
            obj.ID = id;
            var ret = FiltrarPorEntidade(entidade: obj, manterContexto: manterContexto);
            return ret.Result?.FirstOrDefault();
        }

        /// <summary>
        /// Efetua pesquisa genérica da entidade pelos parâmetros informados
        /// </summary>
        /// <param name="entidade">Entidade com o valores que serão considerados para filtro</param>
        /// <param name="outrosFiltros">Action para complementar o filtro da pesquisa com propriedades conhecidade pelo DAO resposnsável pela entidade</param>
        /// <param name="manterContexto">Controle para manter o retorno localizado no cache do Entity ou náo</param>
        /// <returns>Lista de entidade localizada pelo <paramref name="entidade"/> e <paramref name="outrosFiltros"/> informados</returns>
        internal protected Task<List<T>> FiltrarPorEntidade(T entidade, Action<T> outrosFiltros = null, bool manterContexto = true)
        {
            if (entidade == null) throw new ArgumentException("Entidade para filtro não informada");
            var dbSet = _dbContext.Set(entidade.GetType());
            if (!manterContexto)
                dbSet.AsNoTracking();
            query = dbSet.AsQueryable() as IQueryable<T>;
            if (Token.Claim != "ADMIN")
            {
                query = from filtro in query where filtro.UsuarioInclusao == Token.ID select filtro;
                query = from filtro in query where filtro.DataCancelamento == null select filtro;
            }
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
            outrosFiltros?.Invoke(entidade);
            return query.ToListAsync();
        }
    }
}