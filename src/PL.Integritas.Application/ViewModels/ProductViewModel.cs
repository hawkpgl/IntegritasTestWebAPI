namespace PL.Integritas.Application.ViewModels
{
    public class ProductViewModel : EntityBaseViewModel
    {
        public ProductViewModel()
        {
            Active = true;
        }

        public int CartNumber { get; set; }

        #region Properties

        public string Name { get; set; }

        #endregion
    }
}
