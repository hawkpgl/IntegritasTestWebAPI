namespace PL.Integritas.Infra.Data.Migrations
{
    using Domain.Entities;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<PL.Integritas.Infra.Data.Context.IntegritasContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PL.Integritas.Infra.Data.Context.IntegritasContext context)
        {
            //  This method will be called after migrating to the latest version.
                        
            context.Products.AddOrUpdate(
              p => p.Name,
              new Product { Name = "Soda" },
              new Product { Name = "Juice" },
              new Product { Name = "Beer" }
            );
            
        }
    }
}
