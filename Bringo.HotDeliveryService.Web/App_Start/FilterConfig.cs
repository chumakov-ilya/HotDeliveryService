using System.Web;
using System.Web.Mvc;

namespace Bringo.HotDeliveryService.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
