using System.Web;
using System.Web.Mvc;

namespace Levi9ZrOrderingFood
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
