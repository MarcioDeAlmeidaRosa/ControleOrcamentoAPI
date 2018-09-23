using System;
using AutoMapper;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using ControleOrcamentoAPI.Models;
using System.Data.Entity.Validation;
using ControleOrcamentoAPI.Extensoes;
using ControleOrcamentoAPI.Exceptions;
using System.Data.Entity.Infrastructure;

namespace ControleOrcamentoAPI.DAO
{
    /// <summary>
    /// Responsável por manter os dados do usuário
    /// </summary>
    public class UsuarioDAO : DAO<Usuario>, IDAO<Usuario>
    {
        /// <summary>
        /// Responsável por instanciar o objeto
        /// </summary>
        /// <param name="token"> Entidade agência contedo os filtros que serão considerados na consulta</param>
        /// <exception cref="ArgumentNullException"> Excação lançada quando o <paramref name="token"/> é nulo</exception>
        public UsuarioDAO(UsuarioAutenticado token) : base(token) { }

        /// <summary>
        /// Resposnável por atualizar o registro na entidade
        /// </summary>
        /// <param name="id"> ID da entidade para efetuar atualização no banco de dados</param>
        /// <param name="entidade"> Entidade contendo as informações que serão atualizadas no banco de dados</param>
        /// <exception cref="ArgumentException"> Excação lançada quando o <paramref name="entidade"/> da entidade e nula</exception>
        /// <exception cref="RegistroNaoEncontradoException"> Exception lançada quando não localizado o registro</exception>
        /// <exception cref="RegistroUpdateException"> Exception lançada quando acontece algum erro no momento de atualizar o registro</exception>
        /// <returns>Entidade atualizada no banco de dados</returns>
        public Usuario Atualizar(long id, Usuario entidade)
        {
            if (id <= 0)
                throw new ArgumentException("Não informado o id para atualização");
            if (entidade == null)
                throw new ArgumentException("Não informado a entidade para atualização");
            try
            {
                var entidadeLocalizada = ListarPorEntidade(new Usuario() { ID = id }).FirstOrDefault();
                if (entidadeLocalizada == null)
                    throw new RegistroNaoEncontradoException("Usuário não localizado.");
                Mapper.Map(entidade, entidadeLocalizada);
                entidadeLocalizada.DataAlteracao = DateTime.UtcNow;
                entidadeLocalizada.UsuarioAlteracao = Token.ID;
                _dbContext.SaveChanges();
                return BuscarPorID(entidadeLocalizada.ID);
            }
            catch (DbUpdateException ex)
            {
                throw new RegistroUpdateException(string.Format("Usuário já cadastrado com o login {0}", entidade.Login), ex);
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
        public Usuario BuscarPorID(long id)
        {
            if (id <= 0)
                throw new ArgumentException("Não informado o ID para pesquisar");
            var entidadeLocalizada = ListarPorEntidade(new Usuario() { ID = id }).FirstOrDefault();
            if (entidadeLocalizada == null) throw new RegistroNaoEncontradoException("Usuário não localizado.");
            return entidadeLocalizada;
        }

        /// <summary>
        /// Resposnável por incluir novo registro na entidade na coleção
        /// </summary>
        /// <param name="entidade"> Entidade contendo as informações que serão inseridas no banco de dados</param>
        /// <exception cref="ArgumentException"> Excação lançada quando o <paramref name="entidade"/> da entidade e nula</exception>
        /// <exception cref="RegistroInsertException"> Exception lançada ocorre algúm problema na inclusão do registro</exception>
        /// <returns>Entidade incluída no banco de dados</returns>
        public Usuario Criar(Usuario entidade)
        {
            if (entidade == null)
                throw new ArgumentException("Não informado a entidade para inclusão");
            try
            {
                entidade.DataInclusao = DateTime.UtcNow;
                entidade.UsuarioInclusao = Token.ID;
                _dbContext.Usuarios.Add(entidade);
                _dbContext.SaveChanges();
                return BuscarPorID(entidade.ID);
            }
            catch (DbUpdateException ex)
            {
                throw new RegistroInsertException(string.Format("Usuário já cadastrado com o codigo {0}", entidade.Login), ex);
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
                var entidadeLocalizada = ListarPorEntidade(new Usuario() { ID = id }).FirstOrDefault();
                if (entidadeLocalizada == null)
                    throw new RegistroNaoEncontradoException("Usuário não localizado.");
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
        public IList<Usuario> ListarPorEntidade(Usuario entidade)
        {
            if (entidade == null)
                throw new ArgumentException("Entidade para filtro não informada");
            query = from queryFiltro
                      in _dbContext.Usuarios.AsNoTracking()
                    select queryFiltro;
            query = AdicionarFiltrosComuns(entidade);
            if (!string.IsNullOrWhiteSpace(entidade.Nome))
                query = from filtro in query where filtro.Nome.Equals(entidade.Nome, StringComparison.InvariantCultureIgnoreCase) select filtro;
            if (!string.IsNullOrWhiteSpace(entidade.SobreNome))
                query = from filtro in query where filtro.SobreNome.Equals(entidade.SobreNome, StringComparison.InvariantCultureIgnoreCase) select filtro;
            if (!string.IsNullOrWhiteSpace(entidade.Email))
                query = from filtro in query where filtro.Email.Equals(entidade.Email, StringComparison.InvariantCultureIgnoreCase) select filtro;
            if (!string.IsNullOrWhiteSpace(entidade.Login))
                query = from filtro in query where filtro.Login.Equals(entidade.Login, StringComparison.InvariantCultureIgnoreCase) select filtro;
            if (!string.IsNullOrWhiteSpace(entidade.Senha))
                query = from filtro in query where filtro.Senha.Equals(entidade.Senha, StringComparison.InvariantCultureIgnoreCase) select filtro;
            if (!string.IsNullOrWhiteSpace(entidade.Claim))
                query = from filtro in query where filtro.Claim.Equals(entidade.Claim, StringComparison.InvariantCultureIgnoreCase) select filtro;
            if (entidade.DataVerificacao.HasValue)
                query = from filtro in query where filtro.DataVerificacao == entidade.DataVerificacao.Value.ToTimeZoneTime(Token.TimeZone) select filtro;
            if (entidade.Verificado.HasValue)
                query = from filtro in query where filtro.Verificado == entidade.Verificado.Value select filtro;
            if (entidade.Bloqueado.HasValue)
                query = from filtro in query where filtro.Bloqueado == entidade.Bloqueado.Value select filtro;
            if (entidade.DataBloqueio.HasValue)
                query = from filtro in query where filtro.DataBloqueio == entidade.DataBloqueio.Value.ToTimeZoneTime(Token.TimeZone) select filtro;
            if (entidade.UsuarioBloqueio.HasValue)
                query = from filtro in query where filtro.UsuarioBloqueio == entidade.UsuarioBloqueio.Value select filtro;
            if (!string.IsNullOrWhiteSpace(entidade.TimeZone))
                query = from filtro in query where filtro.TimeZone.Equals(entidade.TimeZone, StringComparison.InvariantCultureIgnoreCase) select filtro;
            var result = query.ExecutaFuncao(FuncAjustaTimeZone).OrderBy(p => p.Nome).ThenBy(p => p.SobreNome).ThenBy(p => p.Email).ThenBy(p => p.Login).ToArray();
            if (result == null)
                throw new RegistroNaoEncontradoException("Usuário não localizada.");
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
                if (data.DataVerificacao.HasValue) data.DataVerificacao = data.DataVerificacao.Value.ToTimeZoneTime(Token.TimeZone);
                if (data.DataBloqueio.HasValue) data.DataBloqueio = data.DataBloqueio.Value.ToTimeZoneTime(Token.TimeZone);
                data.Claim = null;
                data.Senha = null;
                data.Salt = null;
                return data;
            };
        }
    }
}
