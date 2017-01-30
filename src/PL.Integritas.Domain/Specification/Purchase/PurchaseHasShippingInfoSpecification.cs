using DomainValidation.Interfaces.Specification;
using PL.Integritas.Domain.Entities;

namespace PL.Integritas.Domain.Specification.PurchaseSpecification
{
    public class PurchaseHasShippingInfoSpecification : ISpecification<Purchase>
    {
        public bool IsSatisfiedBy(Purchase purchase)
        {
            return !string.IsNullOrEmpty(purchase.Adress) && !string.IsNullOrEmpty(purchase.ZipPostalCode);
        }
    }
}
