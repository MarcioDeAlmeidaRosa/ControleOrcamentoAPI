using System;
using AutoMapper;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using ControleOrcamentoAPI.Models;
using System.Data.Entity.Validation;
using ControleOrcamentoAPI.Extensoes;
using ControleOrcamentoAPI.Exceptions;
using System.Data.Entity.Infrastructure;

namespace ControleOrcamentoAPI.DAO
{
    /// <summary>
    /// Responsável por manter os dados do banco
    /// </summary>
    public class BancoDAO : DAO<Banco>, IDAO<Banco>
    {
        /// <summary>
        /// Responsável por instanciar o objeto
        /// </summary>
        /// <param name="token"> Entidade agência contedo os filtros que serão considerados na consulta</param>
        /// <exception cref="ArgumentNullException"> Excação lançada quando o <paramref name="token"/> é nulo</exception>
        public BancoDAO(UsuarioAutenticado token) : base(token) { }

        /// <summary>
        /// Resposnável por atualizar o registro na entidade
        /// </summary>
        /// <param name="id"> ID da entidade para efetuar atualização no banco de dados</param>
        /// <param name="entidade"> Entidade contendo as informações que serão atualizadas no banco de dados</param>
        /// <exception cref="ArgumentException"> Excação lançada quando o <paramref name="entidade"/> da entidade e nula</exception>
        /// <exception cref="RegistroNaoEncontradoException"> Exception lançada quando não localizado o registro</exception>
        /// <exception cref="RegistroUpdateException"> Exception lançada quando acontece algum erro no momento de atualizar o registro</exception>
        /// <returns>Entidade atualizada no banco de dados</returns>
        public Banco Atualizar(long id, Banco entidade)
        {
            if (id <= 0)
                throw new ArgumentException("Não informado o id para atualização");
            if (entidade == null)
                throw new ArgumentException("Não informado a entidade para atualização");
            try
            {
                var entidadeLocalizada = ListarPorEntidade(new Banco() { ID = id }).FirstOrDefault();
                if (entidadeLocalizada == null)
                    throw new RegistroNaoEncontradoException("Banco não localizado.");
                Mapper.Map(entidade, entidadeLocalizada);
                entidadeLocalizada.DataAlteracao = DateTime.UtcNow;
                entidadeLocalizada.UsuarioAlteracao = Token.ID;
                _dbContext.SaveChanges();
                return BuscarPorID(entidadeLocalizada.ID);
            }
            catch (DbUpdateException ex)
            {
                throw new RegistroUpdateException(string.Format("Banco já cadastrado com o codigo {0}", entidade.Codigo), ex);
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder st = new StringBuilder();
                ex.EntityValidationErrors.ToList().ForEach(errs => errs.ValidationErrors.ToList().ForEach(err => st.AppendLine(err.ErrorMessage)));
                throw new RegistroUpdateException(st.ToString(), ex);
            }
        }

        /// <summary>
        /// Resposnável por pesquisar dados por ID
        /// </summary>
        /// <param name="id"> ID da entidade para esquisa no banco de dados</param>
        /// <exception cref="ArgumentException"> Excação lançada quando o <paramref name="id"/> da entidade e 0</exception>
        /// <exception cref="RegistroNaoEncontradoException"> Exception lançada quando não localizado o registro</exception>
        /// <returns>Entidade encontrada no banco de dados pelo ID informado</returns>
        public Banco BuscarPorID(long id)
        {
            if (id <= 0)
                throw new ArgumentException("Não informado o ID para pesquisar");
            var entidadeLocalizada = ListarPorEntidade(new Banco() { ID = id }).FirstOrDefault();
            if (entidadeLocalizada == null) throw new RegistroNaoEncontradoException("Banco não localizado.");
            return entidadeLocalizada;
        }

        /// <summary>
        /// Resposnável por incluir novo registro na entidade na coleção
        /// </summary>
        /// <param name="entidade"> Entidade contendo as informações que serão inseridas no banco de dados</param>
        /// <exception cref="ArgumentException"> Excação lançada quando o <paramref name="entidade"/> da entidade e nula</exception>
        /// <exception cref="RegistroInsertException"> Exception lançada ocorre algúm problema na inclusão do registro</exception>
        /// <returns>Entidade incluída no banco de dados</returns>
        public Banco Criar(Banco entidade)
        {
            if (entidade == null)
                throw new ArgumentException("Não informado a entidade para inclusão");
            try
            {
                entidade.DataInclusao = DateTime.UtcNow;
                entidade.UsuarioInclusao = Token.ID;
                _dbContext.Bancos.Add(entidade);
                _dbContext.SaveChanges();
                return BuscarPorID(entidade.ID);
            }
            catch (DbUpdateException ex)
            {
                throw new RegistroInsertException(string.Format("Banco já cadastrado com o codigo {0}", entidade.Codigo), ex);
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder st = new StringBuilder();
                ex.EntityValidationErrors.ToList().ForEach(errs => errs.ValidationErrors.ToList().ForEach(err => st.AppendLine(err.ErrorMessage)));
                throw new RegistroInsertException(st.ToString(), ex);
            }
        }

        /// <summary>
        /// Resposnável por excluir logicamente o registro informado
        /// </summary>
        /// <param name="id"> Chave primária do registro no banco de dados</param>
        /// <exception cref="ArgumentException"> Excação lançada quando o <paramref name="id"/> é menor igual a 0</exception>
        /// <exception cref="RegistroDeleteException"> Exception lançada ocorre algúm problema na exclusao do registro</exception>
        public void Deletar(long id)
        {
            if (id <= 0)
                throw new ArgumentException("Não informado o ID para deleção");
            try
            {
                var entidadeLocalizada = ListarPorEntidade(new Banco() { ID = id }).FirstOrDefault();
                if (entidadeLocalizada == null)
                    throw new RegistroNaoEncontradoException("Banco não localizado.");
                entidadeLocalizada.DataCancelamento = DateTime.UtcNow;
                entidadeLocalizada.UsuarioCancelamento = Token.ID;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RegistroDeleteException(ex.ToString());
            }
        }

        /// <summary>
        /// Resposnável por pesquisar dados pelos filtros passados
        /// </summary>
        /// <param name="entidade"> Entidade contedo os filtros que serão considerados na consulta</param>
        /// <exception cref="ArgumentException"> Excação lançada quando o <paramref name="entidade"/> é nulo</exception>
        /// <exception cref="RegistroNaoEncontradoException"> Exception lançada quando não localizado o registro</exception>
        /// <returns>Lista dos registros encontrados no banco de dados pelo filtro infomado</returns>
        public IList<Banco> ListarPorEntidade(Banco entidade)
        {
            if (entidade == null)
                throw new ArgumentException("Entidade para filtro não informada");
            query = from queryFiltro
                      in _dbContext.Bancos.AsNoTracking()
                    select queryFiltro;
            query = AdicionarFiltrosComuns(entidade);

            if (!string.IsNullOrWhiteSpace(entidade.Codigo))
                query = from filtro in query where filtro.Codigo.Equals(entidade.Codigo, StringComparison.InvariantCultureIgnoreCase) select filtro;
            if (!string.IsNullOrWhiteSpace(entidade.Nome))
                query = from filtro in query where filtro.Nome.Equals(entidade.Nome, StringComparison.InvariantCultureIgnoreCase) select filtro;

            var result = query.ExecutaFuncao(FuncAjustaTimeZone).OrderBy(p => p.Nome).ToArray();

            if (result == null)
                throw new RegistroNaoEncontradoException("Banco não localizado.");
            return result;
        }

        /// <summary>
        /// Responsavel por definir a função que deverá ser executada para ajustar dados de data e hora conforme Time Zone do usuário
        /// </summary>
        protected override void Configurar()
        {
            FuncAjustaTimeZone = data =>
            {
                if (data.DataAlteracao.HasValue) data.DataAlteracao = data.DataAlteracao.Value.ToTimeZoneTime(Token.TimeZone);
                if (data.DataCancelamento.HasValue) data.DataCancelamento = data.DataCancelamento.Value.ToTimeZoneTime(Token.TimeZone);
                if (data.DataInclusao.HasValue) data.DataInclusao = data.DataInclusao.Value.ToTimeZoneTime(Token.TimeZone);
                return data;
            };
        }
    }
}
