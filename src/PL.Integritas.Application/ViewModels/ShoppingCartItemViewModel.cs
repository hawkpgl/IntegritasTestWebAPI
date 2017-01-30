using System;

namespace PL.Integritas.Application.ViewModels
{
    public class ShoppingCartItemViewModel : EntityBaseViewModel
    {
        public ShoppingCartItemViewModel()
        {
            Active = true;
        }

        #region Properties

        public Int64 ProductId { get; set; }

        public string Name { get; set; }

        public Int64 ShoppingCartId { get; set; }

        #endregion
    }
}
