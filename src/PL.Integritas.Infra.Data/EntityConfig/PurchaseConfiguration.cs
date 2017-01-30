using PL.Integritas.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PL.Integritas.Infra.Data.EntityConfig
{
    public class PurchaseConfiguration : EntityTypeConfiguration<Purchase>
    {
        public PurchaseConfiguration()
        {
            HasKey(p => p.Id);

            Property(p => p.CardHolderName)
                .HasMaxLength(50);

            Property(p => p.CardNumber)
                .IsRequired()
                .HasMaxLength(16);

            Property(p => p.CardExpiryMonth);

            Property(p => p.CardExpiryYear);

            Property(p => p.CVV)
                 .HasMaxLength(3);

            Property(p => p.Adress)
                 .IsRequired()
                 .HasMaxLength(255);

            Property(p => p.Country)
                 .HasMaxLength(50);

            Property(p => p.State)
                 .HasMaxLength(100);

            Property(p => p.City)
                 .HasMaxLength(100);

            Property(p => p.ZipPostalCode)
                 .IsRequired()
                 .HasMaxLength(15);

            Property(p => p.DateRegistered);

            Property(p => p.DateUpdated);

            Property(p => p.Active)
                  .IsRequired();

            Ignore(p => p.ValidationResult);
        }
    }
}
