using System;
using System.Linq;
using System.Data.Entity;
using ControleOrcamentoAPI.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ControleOrcamentoAPI.DataAccess
{
    /// <summary>
    /// Responsável por conter o contexto do banco de dados da aplicação
    /// </summary>
    public class ControleOrcamentoAPIDataContext : DbContext
    {
        //Documentação de definição de schema
        //https://www.tutorialspoint.com/entity_framework/entity_framework_data_annotations.htm

        /// <summary>
        /// Responsável por enviar a classe base a string de conexão que será usada para conexão com o banco de dados
        /// e configurar o comportamento padrão da conexão
        /// </summary>
        public ControleOrcamentoAPIDataContext() : base("ControleOrcamentoAPIDataContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// Responsável por efetuar do encerramento da conexão com o bando de dados
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            //TODO: VALIDAR SE É NECESSÁRIO
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.EnsureTransactionsForFunctionsAndCommands = false;
            Configuration.UseDatabaseNullSemantics = false;
            Configuration.ValidateOnSaveEnabled = false;
            base.Dispose(disposing);
        }

        /// <summary>
        /// Responsável pela configuração do modelo
        /// </summary>
        /// <param name="modelBuilder"> configurador do modelo das tabelas do banco de dados</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        /// <summary>
        /// Responsável por salvar a entidade
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataInclusao") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataInclusao").CurrentValue = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataInclusao").IsModified = false;
                }
            }

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataAlteracao") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataAlteracao").IsModified = false;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataAlteracao").CurrentValue = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }

        /// <summary>
        /// Tabela de Usuário
        /// </summary>
        public DbSet<Usuario> Usuarios { get; set; }

        /// <summary>
        /// Tabela de Banco
        /// </summary>
        public DbSet<Banco> Bancos { get; set; }

        /// <summary>
        /// Tabela de Agência
        /// </summary>
        public DbSet<Agencia> Agencias { get; set; }
    }
}
