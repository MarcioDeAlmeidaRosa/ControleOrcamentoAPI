using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using ControleOrcamentoAPI.Models;
using ControleOrcamentoAPI.Exceptions;

namespace ControleOrcamentoAPI.DAO
{
    public class BancoDAO : DAO<Banco>, IDAO<Banco>
    {
        public Banco Atualizar(Banco entidade, UsuarioAutenticado token)
        {
            try
            {
                var entidadeLocalizada = dbContext.Bancos.Where(data => data.ID == entidade.ID).FirstOrDefault();
                if (entidadeLocalizada == null)
                    throw new RegistroNaoEncontradoException("Banco não localizado.");
                entidadeLocalizada = entidade;
                entidadeLocalizada.DataAlteracao = DateTime.Now;
                entidadeLocalizada.UsuarioAlteracao = token.ID;
                dbContext.SaveChanges();
                return entidadeLocalizada;
            }
            catch (Exception ex)
            {
                throw new RegistroUpdateException(ex.ToString());
            }
        }

        public Banco BuscarPorID(long id)
        {
            var entidadeLocalizada = dbContext.Bancos.Where(data => data.ID == id).FirstOrDefault();
            if (entidadeLocalizada == null)
                throw new RegistroNaoEncontradoException("Banco não localizado.");
            return entidadeLocalizada;
        }

        public Banco Criar(Banco entidade, UsuarioAutenticado token)
        {
            try
            {
                var entidadeLocalizada = dbContext.Bancos.Where(data => data.Codigo.Equals(entidade.Codigo, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                if (entidadeLocalizada != null)
                    throw new RegistroNaoEncontradoException("Banco já cadastrado.");
                entidade.DataInclusao = DateTime.Now;
                entidade.UsuarioInclusao = token.ID;
                dbContext.Bancos.Add(entidade);
                dbContext.SaveChanges();
                return entidade;
            }
            catch (Exception ex)
            {
                throw new RegistroUpdateException(ex.ToString());
            }
        }

        public void Deletar(long id, UsuarioAutenticado token)
        {
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
