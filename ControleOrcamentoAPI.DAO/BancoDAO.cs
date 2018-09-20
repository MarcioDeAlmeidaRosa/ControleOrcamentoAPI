﻿using System;
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
    public class BancoDAO : DAO<Banco>, IDAO<Banco>
    {
        static BancoDAO()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Banco, Banco>().ForMember(m => m.ID, o => o.Ignore());
            });
        }

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

        public Banco BuscarPorID(long id)
        {
            if (id <= 0)
                throw new ArgumentException("Não informado o ID para pesquisar");
            var entidadeLocalizada = dbContext.Bancos.Where(data => data.ID == id).FirstOrDefault();
            if (entidadeLocalizada == null)
                throw new RegistroNaoEncontradoException("Banco não localizado.");
            return entidadeLocalizada;
        }

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
