using System;
using System.ComponentModel.DataAnnotations;

namespace PL.Integritas.Application.ViewModels
{
    public class PurchaseViewModel : EntityBaseViewModel
    {
        public PurchaseViewModel()
        {
            Active = true;
        }

        #region Properties

        public int CartNumber { get; set; }

        public Int64 ShoppingCartId { get; set; }

        public string CardHolderName { get; set; }

        [Required(ErrorMessage = "The card number is required.")]
        [MaxLength(16, ErrorMessage = "Maximum of {0} digits.")]
        public string CardNumber { get; set; }

        public int CardExpiryMonth { get; set; }

        public int CardExpiryYear { get; set; }

        public string CVV { get; set; }

        [Required(ErrorMessage = "The shipping adress is required.")]
        [MaxLength(255, ErrorMessage = "Maximum of {0} characters.")]
        public string Adress { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        [Required(ErrorMessage = "The zip or postal code is required.")]
        [MaxLength(15, ErrorMessage = "Maximum of {0} digits.")]
        public string ZipPostalCode { get; set; }

        #endregion
    }
}
