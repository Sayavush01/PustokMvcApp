using Microsoft.AspNetCore.Mvc;

namespace PustokMvcApp.Areas.Manage.Controllers;
[Area("Manage")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

