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
    public class AgenciaDAO : DAO<Agencia>, IDAO<Agencia>
    {
        static AgenciaDAO()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Agencia, Agencia>().ForMember(m => m.ID, o => o.Ignore());
            });
        }

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

        public Agencia BuscarPorID(long id)
        {
            if (id <= 0)
                throw new ArgumentException("Não informado o ID para pesquisar");

            var entidadeLocalizada = dbContext.Agencias.Where(data => data.ID == id).FirstOrDefault();
            if (entidadeLocalizada == null)
                throw new RegistroNaoEncontradoException("Agência não localizada.");
            return entidadeLocalizada;
        }

        public Agencia Criar(Agencia entidade, UsuarioAutenticado token)
        {
            if (entidade == null)
                throw new ArgumentException("Não informado a entidade para inclusão");
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

        public void Deletar(long id, UsuarioAutenticado token)
        {
            if (id <= 0)
                throw new ArgumentException("Não informado o ID para deleção");
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
                throw new RegistroUpdateException(ex.ToString());
            }
        }

        public IList<Agencia> ListarPorEntidade(Agencia entidade)
        {
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
