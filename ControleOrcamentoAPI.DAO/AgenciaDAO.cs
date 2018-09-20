using System;
using AutoMapper;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using ControleOrcamentoAPI.Models;
using ControleOrcamentoAPI.Exceptions;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace ControleOrcamentoAPI.DAO
{
    /// <summary>
    /// Classe responsável por manter os dados da agência
    /// </summary>
    public class AgenciaDAO : DAO<Agencia>, IDAO<Agencia>
    {
        /// <summary>
        /// Construtor estático responsável pelo mapeamento das classes
        /// </summary>
        static AgenciaDAO()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Agencia, Agencia>().ForMember(m => m.ID, o => o.Ignore());
            });
        }

        /// <summary>
        /// Método resposnável por atualizar o registro na entidade informada de agência
        /// </summary>
        /// <param name="entidade">Entidade agência contendo as informações que serão atualizadas no banco de dados</param>
        /// <param name="token">Usuário logado na aplicação</param>
        /// <exception cref="ArgumentException">Excação lançada quando o <paramref name="entidade"/> da entidade e nula</exception>
        /// <exception cref="RegistroNaoEncontradoException">Exception lançada quando não localizado o registro</exception>
        /// <exception cref="RegistroUpdateException">Exception lançada quando acontece algum erro no momento de atualizar o registro</exception>
        /// <returns>Entidade agência atualizada no banco de dados</returns>
        public Agencia Atualizar(Agencia entidade, UsuarioAutenticado token)
        {
            if (entidade == null)
                throw new ArgumentException("Não informado a entidade para atualização");
            try
            {
                var entidadeLocalizada = dbContext.Agencias.Where(data => data.ID == entidade.ID).FirstOrDefault();
                if (entidadeLocalizada == null)
                    throw new RegistroNaoEncontradoException("Agência não localizada.");
                Mapper.Map(entidade, entidadeLocalizada);
                entidadeLocalizada.DataAlteracao = DateTime.Now;
                entidadeLocalizada.UsuarioAlteracao = token.ID;
                dbContext.SaveChanges();
                return entidadeLocalizada;
            }
            catch (DbUpdateException ex)
            {
                throw new RegistroUpdateException(string.Format("Agência já cadastrado com o número para este banco {0}", entidade.Numero), ex);
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder st = new StringBuilder();
                ex.EntityValidationErrors.ToList().ForEach(errs => errs.ValidationErrors.ToList().ForEach(err => st.AppendLine(err.ErrorMessage)));
                throw new RegistroUpdateException(st.ToString(), ex);
            }
        }

        /// <summary>
        /// Método método resposnável por pesquisar dados por ID da agência
        /// </summary>
        /// <param name="id">Chave primária do registro da agência</param>
        /// <exception cref="ArgumentException">Excação lançada quando o <paramref name="id"/> da entidade e 0</exception>
        /// <exception cref="RegistroNaoEncontradoException">Exception lançada quando não localizado o registro</exception>
        /// <returns>Entidade Agência encontrada no banco de dados pelo ID informado</returns>
        public Agencia BuscarPorID(long id)
        {
            if (id <= 0)
                throw new ArgumentException("Não informado o ID para pesquisar");

            var entidadeLocalizada = dbContext.Agencias.Where(data => data.ID == id).FirstOrDefault();
            if (entidadeLocalizada == null)
                throw new RegistroNaoEncontradoException("Agência não localizada.");
            return entidadeLocalizada;
        }

        /// <summary>
        /// Método resposnável por incluir novo registro na entidade informada de agência
        /// </summary>
        /// <param name="entidade">Entidade contendo as informações que serão inseridas no banco de dados de agência</param>
        /// <param name="token">Usuário logado na aplicação</param>
        /// <exception cref="ArgumentException">Excação lançada quando o <paramref name="entidade"/> da entidade e nula</exception>
        /// <exception cref="ArgumentException">Excação lançada quando o <paramref name="token"/> da entidade e nula</exception>
        /// <exception cref="RegistroInsertException">Exception lançada ocorre algúm problema na inclusão do registro</exception>
        /// <returns>Entidade agência incluída no banco de dados pelo ID informado</returns>
        public Agencia Criar(Agencia entidade, UsuarioAutenticado token)
        {
            if (entidade == null)
                throw new ArgumentException("Não informado a entidade para inclusão");
            if (token == null)
                throw new ArgumentException("Usuário não informado");
            try
            {
                entidade.DataInclusao = DateTime.Now;
                entidade.UsuarioInclusao = token.ID;
                dbContext.Agencias.Add(entidade);
                dbContext.SaveChanges();
                return entidade;
            }
            catch (DbUpdateException ex)
            {
                throw new RegistroInsertException(string.Format("Agência já cadastrado com o número para este banco {0}", entidade.Numero), ex);
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder st = new StringBuilder();
                ex.EntityValidationErrors.ToList().ForEach(errs => errs.ValidationErrors.ToList().ForEach(err => st.AppendLine(err.ErrorMessage)));
                throw new RegistroInsertException(st.ToString(), ex);
            }
        }

        /// <summary>
        /// Método resposnável por excluir logicamente o registro informado de agência
        /// </summary>
        /// <param name="id">Chave primária do registro de agência no banco de dados</param>
        /// <param name="token">Usuário logado na aplicação</param>
        /// <exception cref="ArgumentException">Excação lançada quando o <paramref name="id"/> é menor igual a 0</exception>
        /// <exception cref="ArgumentException">Excação lançada quando o <paramref name="token"/> da entidade e nula</exception>
        /// <exception cref="RegistroDeleteException">Exception lançada ocorre algúm problema na exclusao do registro</exception>
        public void Deletar(long id, UsuarioAutenticado token)
        {
            if (id <= 0)
                throw new ArgumentException("Não informado o ID para deleção");
            if (token == null)
                throw new ArgumentException("Usuário não informado");
            try
            {
                var entidadeLocalizada = dbContext.Agencias.Where(data => data.ID == id).FirstOrDefault();
                if (entidadeLocalizada == null)
                    throw new RegistroNaoEncontradoException("Agência não localizada.");
                entidadeLocalizada.DataCancelamento = DateTime.Now;
                entidadeLocalizada.UsuarioCancelamento = token.ID;
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RegistroDeleteException(ex.ToString());
            }
        }

        /// <summary>
        /// Método resposnável por pesquisar agência dados pelos filtros passados
        /// </summary>
        /// <param name="entidade">Entidade agência contedo os filtros que serão considerados na consulta</param>
        /// <exception cref="ArgumentException">Excação lançada quando o <paramref name="entidade"/> é nulo</exception>
        /// <exception cref="RegistroNaoEncontradoException">Exception lançada quando não localizado o registro</exception>
        /// <returns>Lista dos registros de agência encontrados no banco de dados pelo filtro infomado</returns>
        public IList<Agencia> ListarPorEntidade(Agencia entidade)
        {
            if (entidade == null)
                throw new ArgumentException("Entidade para filtro não informada");
            query = from queryFiltro
                      in dbContext.Agencias.AsNoTracking()
                    where queryFiltro.DataCancelamento == null
                    select queryFiltro;
            query = AdicionarFiltrosComuns(entidade);
            if (!string.IsNullOrWhiteSpace(entidade.Numero))
                query = from filtro in query where filtro.Numero.Equals(entidade.Numero, StringComparison.InvariantCultureIgnoreCase) select filtro;
            if (!string.IsNullOrWhiteSpace(entidade.DV))
                query = from filtro in query where filtro.DV.Equals(entidade.DV, StringComparison.InvariantCultureIgnoreCase) select filtro;
            if (entidade.BancoID > 0)
                query = from filtro in query where filtro.BancoID == entidade.BancoID select filtro;
            var result = query.ToArray().OrderBy(p => p.Numero).ToArray();
            if (result == null)
                throw new RegistroNaoEncontradoException("Agência não localizada.");
            return result;
        }

    }
}
