using PL.Integritas.Domain.Entities;
using PL.Integritas.Infra.Data.Context;
using System.Data.Entity.ModelConfiguration;

namespace PL.Integritas.Infra.Data.EntityConfig
{
    public class ShoppingCartItemConfiguration : EntityTypeConfiguration<ShoppingCartItem>
    {
        public ShoppingCartItemConfiguration()
        {
            HasKey(p => p.Id);

            Property(p => p.DateRegistered);

            Property(p => p.DateUpdated);

            Property(p => p.Active)
                .IsRequired();

            Ignore(p => p.ValidationResult);
        }
    }
}
