using PL.Integritas.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PL.Integritas.Infra.Data.EntityConfig
{
    public class ShoppingCartConfiguration : EntityTypeConfiguration<ShoppingCart>
    {
        public ShoppingCartConfiguration()
        {
            HasKey(p => p.Id);

            Property(p => p.Number);

            Property(p => p.DateRegistered);

            Property(p => p.DateUpdated);

            Property(p => p.Active)
                .IsRequired();

            Ignore(p => p.ValidationResult);
        }
    }
}
