using PlataformaVIA.Presentacion.Filters;
using System.Web;
using System.Web.Mvc;

namespace PlataformaVIA.Presentacion
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new SecurityLogAttribute());
        }
    }
}
