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
    /// Classe responsável por manter os dados do banco
    /// </summary>
    public class BancoDAO : DAO<Banco>, IDAO<Banco>
    {
        /// <summary>
        /// Construtor estático responsável pelo mapeamento das classes
        /// </summary>
        static BancoDAO()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Banco, Banco>().ForMember(m => m.ID, o => o.Ignore());
            });
        }

        /// <summary>
        /// Método resposnável por atualizar o registro na entidade informada de banco
        /// </summary>
        /// <param name="entidade">Entidade banco contendo as informações que serão atualizadas no banco de dados</param>
        /// <param name="token">Usuário logado na aplicação</param>
        /// <returns>Entidade banco atualizada no banco de dados</returns>
        public Banco Atualizar(Banco entidade, UsuarioAutenticado token)
        {
            if (entidade == null)
                throw new ArgumentException("Não informado a entidade para atualização");
            try
            {
                var entidadeLocalizada = dbContext.Bancos.Where(data => data.ID == entidade.ID).FirstOrDefault();
                if (entidadeLocalizada == null)
                    throw new RegistroNaoEncontradoException("Banco não localizado.");
                Mapper.Map(entidade, entidadeLocalizada);
                entidadeLocalizada.DataAlteracao = DateTime.Now;
                entidadeLocalizada.UsuarioAlteracao = token.ID;
                dbContext.SaveChanges();
                return entidadeLocalizada;
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
        /// Método método resposnável por pesquisar dados por ID do banco
        /// </summary>
        /// <param name="id">Chave primária do registro do banco</param>
        /// <returns>Entidade Banco encontrada no banco de dados pelo ID informado</returns>
        public Banco BuscarPorID(long id)
        {
            if (id <= 0)
                throw new ArgumentException("Não informado o ID para pesquisar");
            var entidadeLocalizada = dbContext.Bancos.Where(data => data.ID == id).FirstOrDefault();
            if (entidadeLocalizada == null)
                throw new RegistroNaoEncontradoException("Banco não localizado.");
            return entidadeLocalizada;
        }

        /// <summary>
        /// Método resposnável por incluir novo registro na entidade informada de banco
        /// </summary>
        /// <param name="entidade">Entidade contendo as informações que serão inseridas no banco de dados de banco</param>
        /// <param name="token">Usuário logado na aplicação</param>
        /// <returns>Entidade banco incluída no banco de dados pelo ID informado</returns>
        public Banco Criar(Banco entidade, UsuarioAutenticado token)
        {
            if (entidade == null)
                throw new ArgumentException("Não informado a entidade para inclusão");
            try
            {
                entidade.DataInclusao = DateTime.Now;
                entidade.UsuarioInclusao = token.ID;
                dbContext.Bancos.Add(entidade);
                dbContext.SaveChanges();
                return entidade;
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
        /// Método resposnável por excluir logicamente o registro informado de banco
        /// </summary>
        /// <param name="id">Chave primária do registro de banco no banco de dados</param>
        /// <param name="token">Usuário logado na aplicação</param>
        public void Deletar(long id, UsuarioAutenticado token)
        {
            if (id <= 0)
                throw new ArgumentException("Não informado o ID para deleção");
            try
            {
                var entidadeLocalizada = dbContext.Bancos.Where(data => data.ID == id).FirstOrDefault();
                if (entidadeLocalizada == null)
                    throw new RegistroNaoEncontradoException("Banco não localizado.");
                entidadeLocalizada.DataCancelamento = DateTime.Now;
                entidadeLocalizada.UsuarioCancelamento = token.ID;
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RegistroUpdateException(ex.ToString());
            }
        }

        /// <summary>
        /// Método resposnável por pesquisar banco dados pelos filtros passados
        /// </summary>
        /// <param name="entidade">Entidade agência contedo os filtros que serão considerados no banco</param>
        /// <returns>Lista dos registros de bancos encontrados no banco de dados pelo filtro infomado</returns>
        public IList<Banco> ListarPorEntidade(Banco entidade)
        {
            query = from queryFiltro
                      in dbContext.Bancos.AsNoTracking()
                    where queryFiltro.DataCancelamento == null
                    select queryFiltro;
            query = AdicionarFiltrosComuns(entidade);
            if (!string.IsNullOrWhiteSpace(entidade.Codigo))
                query = from filtro in query where filtro.Codigo.Equals(entidade.Codigo, StringComparison.InvariantCultureIgnoreCase) select filtro;
            if (!string.IsNullOrWhiteSpace(entidade.Nome))
                query = from filtro in query where filtro.Nome.Equals(entidade.Nome, StringComparison.InvariantCultureIgnoreCase) select filtro;
            var result = query.ToArray().OrderBy(p => p.Nome).ToArray();
            if (result == null)
                throw new RegistroNaoEncontradoException("Banco não localizado.");
            return result;
        }

    }
}
