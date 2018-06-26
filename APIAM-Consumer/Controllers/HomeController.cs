using System.Web.Mvc;
using OktaAPI.Helpers;

namespace APIAM_Consumer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult SubmitCredentials()
        {
            var tokenresponse = APIHelper.GetToken();

            if (string.IsNullOrEmpty(tokenresponse.AccessToken))
            {
                try
                {
                    var errordesc = "Unknown Error - No Access Token created";
                    errordesc = tokenresponse.errorSummary;
                    TempData["Message"] = errordesc;
                    TempData["IsError"] = true;
                }
                catch
                {
                    // ignored
                }
            }
            else
            {
                Helpers.LoginHelper.SetOIDCTokens(tokenresponse);
            }
            
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CallPublic()
        {
            return RedirectToAction("Public", "APIService");
        }
        
        public ActionResult CallPrivate()
        {
            //Global.asax adds the Access Token

            return RedirectToAction("Private", "APIService");
        }

        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult RevokeToken()
        {
            Helpers.LoginHelper.RevokeToken();

            return RedirectToAction("Index", "Home");
        }
    }
}