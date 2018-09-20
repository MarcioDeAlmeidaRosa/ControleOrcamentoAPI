using System;
using System.Linq;
using System.Data.Entity;
using ControleOrcamentoAPI.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ControleOrcamentoAPI.DataAccess
{
    public class ControleOrcamentoAPIDataContext: DbContext
    {
        //Documentação de definição de schema
        //https://www.tutorialspoint.com/entity_framework/entity_framework_data_annotations.htm
        
        public ControleOrcamentoAPIDataContext(): base("ControleOrcamentoAPIDataContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void Dispose(bool disposing)
        {
            //TODO: VALIDAR SE É NECESSÁRIO
            //Configuration.LazyLoadingEnabled = false;
            //Configuration.ProxyCreationEnabled = false;
            //Configuration.AutoDetectChangesEnabled = false;
            //Configuration.EnsureTransactionsForFunctionsAndCommands = false;
            //Configuration.UseDatabaseNullSemantics = false;
            //Configuration.ValidateOnSaveEnabled = false;

            base.Dispose(disposing);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataInclusao") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataInclusao").CurrentValue = DateTime.Today;
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
                    entry.Property("DataAlteracao").CurrentValue = DateTime.Today;
                }
            }

            return base.SaveChanges();
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<Agencia> Agencias { get; set; }
    }
}
