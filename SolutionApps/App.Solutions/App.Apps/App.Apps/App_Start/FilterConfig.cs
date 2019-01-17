using System.Web;
using System.Web.Mvc;

namespace App.Apps
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new DisableCache());
        }
    }
}