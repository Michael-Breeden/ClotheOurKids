using ClotheOurKids.Web.CustomClasses;
using System.Web;
using System.Web.Mvc;

namespace ClotheOurKids.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new RequireHttpsAttribute());

            if (WebsiteAvailable.IsOffline)
            {
                filters.Add(new OfflineMessageAttribute());
            }
        }
    }
}
