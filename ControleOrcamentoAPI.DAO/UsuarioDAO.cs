using System;
using AutoMapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<Usuario> Atualizar(long id, Usuario entidade)
        {
            if (id <= 0) throw new ArgumentException("Não informado o id para atualização");
            if (entidade == null) throw new ArgumentException("Não informado a entidade para atualização");
            try
            {
                var entidadeLocalizada = await FiltrarPorID(id, true);
                if (entidadeLocalizada == null) throw new RegistroNaoEncontradoException("Usuário não localizado.");
                Mapper.Map(entidade, entidadeLocalizada);
                entidadeLocalizada.DataAlteracao = DateTime.UtcNow;
                entidadeLocalizada.UsuarioAlteracao = Token.ID;
                await _dbContext.SaveChangesAsync();
                return await FiltrarPorID(entidadeLocalizada.ID);
            }
            catch (DbUpdateException ex)
            {
                throw new RegistroUpdateException(string.Format("Usuário já cadastrado com o número para este banco {0}", entidade.Login), ex);
            }
            catch (DbEntityValidationException ex)
            {
                var st = new StringBuilder();
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
        public async Task<Usuario> BuscarPorID(long id)
        {
            if (id <= 0) throw new ArgumentException("Não informado o ID para pesquisar");
            var entidadeLocalizada = await FiltrarPorID(id);
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
        public async Task<Usuario> Criar(Usuario entidade)
        {
            if (entidade == null) throw new ArgumentException("Não informado a entidade para inclusão");
            try
            {
                entidade.DataInclusao = DateTime.UtcNow;
                entidade.UsuarioInclusao = Token.ID;
                _dbContext.Usuarios.Add(entidade);
                await _dbContext.SaveChangesAsync();
                return await FiltrarPorID(entidade.ID);
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
        /// <returns>Task para chamada assíncrona</returns>
        public async Task Deletar(long id)
        {
            if (id <= 0) throw new ArgumentException("Não informado o ID para deleção");
            var entidadeLocalizada = await FiltrarPorID(id, true);
            if ((entidadeLocalizada == null) || (entidadeLocalizada.DataCancelamento.HasValue))
                throw new RegistroNaoEncontradoException("Usuário não localizado.");
            entidadeLocalizada.DataAlteracao = DateTime.UtcNow;
            entidadeLocalizada.UsuarioAlteracao = Token.ID;
            entidadeLocalizada.DataCancelamento = DateTime.UtcNow;
            entidadeLocalizada.UsuarioCancelamento = Token.ID;
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Resposnável por pesquisar dados pelos filtros passados
        /// </summary>
        /// <param name="entidade"> Entidade contedo os filtros que serão considerados na consulta</param>
        /// <exception cref="ArgumentException"> Excação lançada quando o <paramref name="entidade"/> é nulo</exception>
        /// <exception cref="RegistroNaoEncontradoException"> Exception lançada quando não localizado o registro</exception>
        /// <returns>Lista dos registros encontrados no banco de dados pelo filtro infomado</returns>
        public async Task<IList<Usuario>> ListarPorEntidade(Usuario entidade)
        {
            var result = await FiltrarPorEntidade(entidade, new Action<Usuario>(_entidade =>
            {
                if (!string.IsNullOrWhiteSpace(_entidade.Nome))
                    query = from filtro in query where filtro.Nome.EqualsIgnoreCase(_entidade.Nome) select filtro;
                if (!string.IsNullOrWhiteSpace(_entidade.SobreNome))
                    query = from filtro in query where filtro.SobreNome.EqualsIgnoreCase(_entidade.SobreNome) select filtro;
                if (!string.IsNullOrWhiteSpace(_entidade.Email))
                    query = from filtro in query where filtro.Email.EqualsIgnoreCase(_entidade.Email) select filtro;
                if (!string.IsNullOrWhiteSpace(_entidade.Login))
                    query = from filtro in query where filtro.Login.EqualsIgnoreCase(_entidade.Login) select filtro;
                if (!string.IsNullOrWhiteSpace(_entidade.Senha))
                    query = from filtro in query where filtro.Senha.EqualsIgnoreCase(_entidade.Senha) select filtro;
                if (!string.IsNullOrWhiteSpace(_entidade.Claim))
                    query = from filtro in query where filtro.Claim.EqualsIgnoreCase(_entidade.Claim) select filtro;
                if (_entidade.DataVerificacao.HasValue)
                    query = from filtro in query where filtro.DataVerificacao == _entidade.DataVerificacao.Value.ToTimeZoneTime(Token.TimeZone) select filtro;
                if (_entidade.Verificado.HasValue)
                    query = from filtro in query where filtro.Verificado == _entidade.Verificado.Value select filtro;
                if (_entidade.Bloqueado.HasValue)
                    query = from filtro in query where filtro.Bloqueado == _entidade.Bloqueado.Value select filtro;
                if (_entidade.DataBloqueio.HasValue)
                    query = from filtro in query where filtro.DataBloqueio == _entidade.DataBloqueio.Value.ToTimeZoneTime(Token.TimeZone) select filtro;
                if (_entidade.UsuarioBloqueio.HasValue)
                    query = from filtro in query where filtro.UsuarioBloqueio == _entidade.UsuarioBloqueio.Value select filtro;
                if (!string.IsNullOrWhiteSpace(_entidade.TimeZone))
                    query = from filtro in query where filtro.TimeZone.EqualsIgnoreCase(_entidade.TimeZone) select filtro;

            }), false);
            if ((result == null) || (result.Count < 1)) throw new RegistroNaoEncontradoException("Usuário não localizado");
            return result.OrderBy(p => p.Nome).ThenBy(p => p.SobreNome).ThenBy(p => p.Email).ThenBy(p => p.Login).ToArray();
        }
    }
}
