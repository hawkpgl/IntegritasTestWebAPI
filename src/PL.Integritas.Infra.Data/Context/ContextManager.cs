using PL.Integritas.Infra.Data.Interfaces;
using System.Web;

namespace PL.Integritas.Infra.Data.Context
{
    public class ContextManager : IContextManager
    {
        private const string ContextKey = "ContextManager.Context";

        public IntegritasContext GetContext()
        {
            if (HttpContext.Current.Items[ContextKey] == null)
            {
                HttpContext.Current.Items[ContextKey] = new IntegritasContext();
            }

            return (IntegritasContext)HttpContext.Current.Items[ContextKey];
        }
    }
}
