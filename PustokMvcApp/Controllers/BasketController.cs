using Microsoft.AspNetCore.Mvc;

namespace MVC_WEB_APP.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SetCookie()
        {
            Response.Cookies.Append("Pustok", "Hello Pustok");
            return Content("Cookie has been set!");
        }
        public IActionResult GetCookie()
        {
            var cookieValue = Request.Cookies["Pustok"];
            if (cookieValue != null)
            {
                return Content($"Cookie Value: {cookieValue}");
            }
            else
            {
                return Content("Cookie not found.");
            }
        }
        public IActionResult SetSession()
        {
            HttpContext.Session.SetString("PustokSession", "Hello Pustok Session");
            return Content("Session has been set!");
        }

    }
}
