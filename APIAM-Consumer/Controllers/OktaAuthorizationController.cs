using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace APIAM_Consumer.Controllers
{
    public class OktaAuthorizationAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var headers = httpContext.Request.Headers;
            if (!string.IsNullOrEmpty(headers["Bearer"]))
            {
                var accesstoken = headers["Bearer"];
                return Helpers.LoginHelper.IsValidToken(accesstoken);
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "Home",
                        action = "Unauthorized"
                    })
                );
        }
    }
}