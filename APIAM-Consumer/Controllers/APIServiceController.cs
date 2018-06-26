using System.Web.Mvc;

//Simulate a web endpoint receiving of the Request w/Access Token in Header

namespace APIAM_Consumer.Controllers
{
    public class APIServiceController : Controller
    {
        public ActionResult Public()
        {
            return View();
        }

        [OktaAuthorization]
        public ActionResult Private()
        {
            return View();
        }
    }
}