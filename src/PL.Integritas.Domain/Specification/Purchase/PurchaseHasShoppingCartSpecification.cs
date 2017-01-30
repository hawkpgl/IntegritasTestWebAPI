using DomainValidation.Interfaces.Specification;
using PL.Integritas.Domain.Entities;

namespace PL.Integritas.Domain.Specification.PurchaseSpecification
{
    public class PurchaseHasShoppingCartSpecification : ISpecification<Purchase>
    {
        public bool IsSatisfiedBy(Purchase purchase)
        {
            return purchase.ShoppingCart != null;
        }
    }
}
