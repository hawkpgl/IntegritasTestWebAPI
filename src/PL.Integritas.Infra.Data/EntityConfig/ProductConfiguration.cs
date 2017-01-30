using PL.Integritas.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PL.Integritas.Infra.Data.EntityConfig
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            HasKey(p => p.Id);

            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(p => p.DateRegistered);

            Property(p => p.DateUpdated);

            Property(p => p.Active)
                .IsRequired();

            Ignore(p => p.ValidationResult);
        }
    }
}
