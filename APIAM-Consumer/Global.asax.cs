using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace APIAM_Consumer
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //TODO: same for all users - not for PROD
            var sAccessToken = Helpers.LoginHelper.GetAccessToken();
            if (!string.IsNullOrEmpty(sAccessToken))
            {
                HttpContext.Current.Request.Headers.Add("Bearer", sAccessToken);
            }
        }
    }
}
