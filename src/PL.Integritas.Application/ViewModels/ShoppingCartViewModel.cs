using System.Collections.Generic;

namespace PL.Integritas.Application.ViewModels
{
    public class ShoppingCartViewModel : EntityBaseViewModel
    {
        public ShoppingCartViewModel()
        {
            Active = true;
        }

        public int Number { get; set; }

        #region Properties

        public virtual IEnumerable<ShoppingCartItemViewModel> ItemsViewModel { get { return this.items; } }

        #endregion

        #region Fields

        private IList<ShoppingCartItemViewModel> items = new List<ShoppingCartItemViewModel>();

        #endregion

        #region Methods

        public virtual ShoppingCartViewModel AddItem(ShoppingCartItemViewModel item)
        {
            if (!items.Contains(item))
            {
                items.Add(item);
            }

            return this;
        }

        public virtual ShoppingCartViewModel RemoveItem(ShoppingCartItemViewModel item)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
            }

            return this;
        }

        #endregion
    }
}
