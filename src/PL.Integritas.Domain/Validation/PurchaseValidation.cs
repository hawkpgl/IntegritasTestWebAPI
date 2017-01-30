using DomainValidation.Validation;
using PL.Integritas.Domain.Entities;
using PL.Integritas.Domain.Specification.PurchaseSpecification;

namespace PL.Integritas.Domain.Validation
{
    public class PurchaseValidation : Validator<Purchase>
    {
        public PurchaseValidation()
        {
            var purchaseHasShoppingCartValid = new PurchaseHasShoppingCartSpecification();
            var purchaseHasShippingInfoValid = new PurchaseHasShippingInfoSpecification();
            var purchaseHasCreditCardValid = new PurchaseHasCreditCardSpecification();

            base.Add("purchaseHasShoppingCartValid", new Rule<Purchase>(purchaseHasShoppingCartValid, "The purchase must have a shopping cart."));
            base.Add("purchaseHasShippingInfoValid", new Rule<Purchase>(purchaseHasShippingInfoValid, "The purchase must have a minimal shipping information."));
            base.Add("purchaseHasCreditCardValid", new Rule<Purchase>(purchaseHasCreditCardValid, "The purchase must have a credit card number."));
        }
    }
}
