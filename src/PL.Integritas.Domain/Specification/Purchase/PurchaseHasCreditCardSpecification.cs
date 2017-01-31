using DomainValidation.Interfaces.Specification;
using PL.Integritas.Domain.Entities;

namespace PL.Integritas.Domain.Specification.PurchaseSpecification
{
    public class PurchaseHasCreditCardSpecification : ISpecification<Purchase>
    {
        public bool IsSatisfiedBy(Purchase purchase)
        {
            return !string.IsNullOrEmpty(purchase.CardHolderName);
        }
    }
}
