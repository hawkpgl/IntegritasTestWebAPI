using PL.Integritas.Domain.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace PL.Integritas.Infra.Data.Context
{
    public class IntegritasContext : DbContext
    {
        public IntegritasContext()
            : base("IntegritasContext")
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(IntegritasContext).Assembly);

            #region Conventions

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            #endregion

            #region Mappings
            // Made with Fluent API on EntityConfig folder
            #endregion

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("nvarchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(255));

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DateRegistered") != null &&
                                                                         entry.Entity.GetType().GetProperty("DateUpdated") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DateRegistered").CurrentValue = DateTime.Now;
                    entry.Property("DateUpdated").IsModified = false;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DateUpdated").CurrentValue = DateTime.Now;
                    entry.Property("DateRegistered").IsModified = false;
                }

            }

            return base.SaveChanges();
        }
    }
}
